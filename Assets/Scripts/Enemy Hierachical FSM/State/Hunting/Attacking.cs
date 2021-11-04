using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : Hunting
{
    public Attacking(EnemySM stateMachine) : base("Attacking", stateMachine)
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

        // transition to 


        if (_sm.friendlyFound == false)
        {
            stateMachine.ChangeState(_sm.idleState);
        }

        if ((_sm.friendlyDistance > _sm.friendlyUnStickDistance) && (_sm.friendlyDistance < _sm.friendlyAttackDistance))
        {
            _sm.rigidBody.velocity = -_sm.friendlyDirection;
        }

        if (_sm.friendlyDistance > 3)
        {
            stateMachine.ChangeState(_sm.movingState);
        }

    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // unstick fox from friendlies if too close due to AABB bug

    }
}

