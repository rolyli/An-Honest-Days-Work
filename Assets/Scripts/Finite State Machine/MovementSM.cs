using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    // states
    public Idle idleState;
    public Moving movingState;
    public Attacking attackingState;


    // shared variables
    public GameObject[] friendlies;
    public GameObject friendlyFound;
    public float friendlyDistance;
    public Vector3 friendlyDirection;
    public float friendlyAggroDistance = 10.0f;
    public float friendlyAttackDistance = 3.0f;
    public float friendlyUnStickDistance = 2.9f;

    public Vector3 velocity;
    public RigidBody rigidBody;


    private void Awake()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
        attackingState = new Attacking(this);


        rigidBody = GetComponent<RigidBody>();

    }



    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        velocity = GetComponent<RigidBody>().velocity;

        friendlies = GameObject.FindGameObjectsWithTag("Friendly");

        // Check if near a friendly, e.g. chicken
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
