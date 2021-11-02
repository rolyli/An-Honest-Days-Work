using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Hunting
{
    private float _horizontalInput;

    public Idle(MovementSM stateMachine) : base("Idle", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _sm.rigidBody.velocity = Vector3.zero;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _horizontalInput = Input.GetAxis("Horizontal");

        // transition to moving state

        
        if (_sm.friendlyFound == true)
        {
            stateMachine.ChangeState(_sm.movingState);
        }        
    }
}
