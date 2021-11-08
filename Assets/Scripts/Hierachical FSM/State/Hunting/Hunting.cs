using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunting : BaseState
{
    protected EnemySM _sm;


    public Hunting(string name, EnemySM stateMachine) : base(name, stateMachine)
    {
        _sm = stateMachine;
    }

    public override void UpdateLogic()
    {
        // to do:Stochastic behavior when fox finds animals but is also near the player
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
