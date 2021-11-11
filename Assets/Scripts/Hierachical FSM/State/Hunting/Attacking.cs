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

        _sm.ChangeStateDialogue("RAWR!!!");
        _sm.rigidBody.velocity = Vector3.zero;
        
        


}

private IEnumerator WaitThenChangeState(BaseState state, float time)
    {
        yield return new WaitForSeconds(time);
        stateMachine.ChangeState(state);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();


        // transition

        if (_sm.friendlyFound == false)
        {
            stateMachine.ChangeState(_sm.idleState);
        }

        // Rigid bodies sticking to each other sometimes so this is needed to keep fox at a distance from friendlies
        if ((_sm.friendlyDistance > _sm.friendlyUnStickDistance) && (_sm.friendlyDistance < _sm.friendlyAttackDistance))
        {
            _sm.rigidBody.velocity = Vector3.zero;
        }

        if (_sm.friendlyDistance > 3)
        {
            // Wait then change state, otherwise it will flip back and forth between states too quickly
            
            // stateMachine.ChangeState(_sm.movingState);
            WaitThenChangeState(_sm.movingState, 3);

        }

    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}

