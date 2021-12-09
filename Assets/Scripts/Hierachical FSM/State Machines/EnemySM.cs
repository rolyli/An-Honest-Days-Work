using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : StateMachine
{
    // states
    public Idle idleState;
    public Moving movingState;
    public Attacking attackingState;
    public Fleeing fleeingState;
    public string stateName;

    // shared variables
    // for game logic
    private float maxHealth;
    public float health = 1000.0f;

    // for movement calculations
    public GameObject[] friendlies;
    public GameObject friendlyFound;
    public float friendlyDistance;
    public Vector3 friendlyDirection;
    public float friendlyAggroDistance = 20.0f;
    public float friendlyAttackDistance = 3.0f;
    public float friendlyUnStickDistance = 2.9f;

    public float playerDistance;
    public Vector3 playerDirection;
    public float playerFleeDistance = 7.0f;

    public Vector3 velocity;
    public RigidBody rigidBody;
    GameObject player;
    GameObject friendlyAI;

    bool showStateName = true;
    public GameObject textPrefab;
    public GameObject stateNameGO;

    public void ChangeStateDialogue(string text)
    {
        stateNameGO.GetComponent<TMPro.TextMeshPro>().SetText(text);
    }

    private void Awake()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
        attackingState = new Attacking(this);
        fleeingState = new Fleeing(this);

        if (showStateName == true)
        {
            stateNameGO = Instantiate(textPrefab, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z)
            , Quaternion.identity);

            // Make sure text gets deleted when parent (enemy) gets deleted
            stateNameGO.transform.SetParent(gameObject.transform);
            
            //Debug.Log(stateNameGO.name);
            //Debug.Log("Created: " + stateNameGO.GetComponent<TMPro.TextMeshPro>().text);
        }

        maxHealth = health;
        rigidBody = gameObject.GetComponent<RigidBody>();
        player = GameObject.FindGameObjectWithTag("Player");
        friendlyAI = GameObject.FindGameObjectWithTag("FriendlyAI");


        // Initialize status text above enemy

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        stateName = currentState.name;

        rigidBody = gameObject.GetComponent<RigidBody>();
        velocity = GetComponent<RigidBody>().velocity;

        friendlies = GameObject.FindGameObjectsWithTag("Friendly");

        // Check collision, calculate hitpoint reductions
        foreach (Collision collision in rigidBody.collisionBuffer)
        {
            if (collision.CheckReference(player) || collision.CheckReference(friendlyAI))
            {

                health -= Mathf.Abs(collision.Impulse * 10);
            }
        }

        if (health < 0)
        {
            Object.Destroy(gameObject);
        }

        // Calculate relationship to friendlies and set friendlyFound depending on friendlyDistance
        friendlyFound = null;

        foreach (GameObject friendly in friendlies)
        {
            friendlyDirection = (friendly.transform.position - transform.position).normalized;
            friendlyDistance = (friendly.transform.position - transform.position).magnitude;

            if (friendlyDistance < friendlyAggroDistance)
            {
                friendlyFound = friendly;
            }
        }

        // Calculate relationship to player
        playerDistance = (player.transform.position - transform.position).magnitude;
        playerDirection = (player.transform.position - transform.position).normalized;

        // Reduce prefab scale with proportion to health
        transform.localScale = new Vector3(Mathf.Lerp(0.5f, 1.0f, health / maxHealth), Mathf.Lerp(0.5f, 1.0f, health / maxHealth), Mathf.Lerp(0.5f, 1.0f, health / maxHealth));

    }

    protected override void Update()
    {
        base.Update();
        
        // Update so that state name text is above the enemy
        stateNameGO.transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    /*
    private void OnGUI()
    {
        string name = currentState != null ? currentState.name : "(no current state)";
        string velocityString = currentState != null ? velocity.ToString() : "null";
        string friendlyFoundString = friendlyFound != null ? friendlyFound.name : "False (null)";

        GUILayout.Label("Debugging information: ");
        GUILayout.Label($"FSM State: {name}");
        GUILayout.Label($"Velocity: {velocityString}");
        GUILayout.Label($"Friendly found: {friendlyFoundString}");
        GUILayout.Label($"Friendly distance: {friendlyDistance}");


    }
    */
}
