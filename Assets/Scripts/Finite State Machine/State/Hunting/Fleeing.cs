using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleeing : BaseState
{
    protected MovementSM _sm;


    public Fleeing(MovementSM stateMachine) : base("Fleeing", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void UpdateLogic()
    {
        if (_sm.playerDistance > _sm.playerFleeDistance)
        {
            stateMachine.ChangeState(_sm.idleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.rigidBody.velocity = -_sm.playerDirection;
    }

}

