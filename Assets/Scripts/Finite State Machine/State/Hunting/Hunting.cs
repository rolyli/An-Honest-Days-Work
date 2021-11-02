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
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

}
