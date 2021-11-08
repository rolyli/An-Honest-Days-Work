using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : Hunting
{
    private float _horizontalInput;

    public Moving(EnemySM stateMachine) : base("Moving", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0f;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _horizontalInput = Input.GetAxis("Horizontal");
        // transition to moving state


        if (_sm.friendlyFound == false)
        {
            stateMachine.ChangeState(_sm.idleState);
        } 

        if (_sm.friendlyDistance < 3)
        {
            stateMachine.ChangeState(_sm.attackingState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();


        _sm.rigidBody.velocity = _sm.friendlyDirection.normalized;

        
        Debug.Log(_sm.friendlyDirection.normalized);
    }
}
