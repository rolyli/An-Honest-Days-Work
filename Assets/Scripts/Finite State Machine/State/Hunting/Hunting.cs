using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunting : BaseState
{
    protected MovementSM _sm;


    public Hunting(string name, MovementSM stateMachine) : base(name, stateMachine)
    {
        _sm = stateMachine;
    }

    public override void UpdateLogic()
    {
        // Stochastic behavior when fox finds animals but is also near the player
        base.UpdateLogic();

        if (_sm.playerDistance < _sm.playerFleeDistance)
        {
            stateMachine.ChangeState(_sm.fleeingState);
        }




    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

}
