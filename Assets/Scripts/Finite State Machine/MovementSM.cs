using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    // states
    public Idle idleState;
    public Moving movingState;
    public Attacking attackingState;
    public Fleeing fleeingState;

    // shared variables
    public GameObject[] friendlies;
    public GameObject friendlyFound;
    public float friendlyDistance;
    public Vector3 friendlyDirection;
    public float friendlyAggroDistance = 20f;
    public float friendlyAttackDistance = 3.0f;
    public float friendlyUnStickDistance = 2.9f;

    public float playerDistance;
    public Vector3 playerDirection;
    public float playerFleeDistance = 7.0f;

    public Vector3 velocity;
    public RigidBody rigidBody;


    private void Awake()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
        attackingState = new Attacking(this);
        fleeingState = new Fleeing(this);



        rigidBody = GetComponent<RigidBody>();

    }



    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        velocity = GetComponent<RigidBody>().velocity;

        friendlies = GameObject.FindGameObjectsWithTag("Friendly");

        // Calculate relationship to friendlies and set friendlyFound depending on friendlyDistance
        friendlyFound = null;

        foreach (GameObject friendly in friendlies)
        {
            friendlyDirection = (friendly.transform.position - transform.position).normalized;
            friendlyDistance = (friendly.transform.position - transform.position).magnitude;
            Debug.Log(friendlyAggroDistance);

            if (friendlyDistance < friendlyAggroDistance)
            {
                friendlyFound = friendly;
            }
        }

        // Calculate relationship to player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDistance = (player.transform.position - transform.position).magnitude;
        playerDirection = (player.transform.position - transform.position).normalized;
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

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
}
