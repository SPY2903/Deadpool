using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRunState : IState
{
    private BossController bossController;

    public BRunState(BossController bossController)
    {
        this.bossController = bossController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        bossController.Anim.CrossFade("Run", .25f);
        bossController.navMesh.speed = bossController.crtDetail.speedUp;
        bossController.navMesh.acceleration = 1200;
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        bossController.Move(bossController.Target);
        if (bossController.isAtTarget)
        {
            if(bossController.currentAttack == 0)
            {
                bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bAngryAttackStateOne);
            }
            if(bossController.currentAttack == 1)
            {
                bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bAngryAttackStateTwo);
            }
            if(bossController.currentAttack == 2)
            {
                bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bJumpAttackState);
            }
        }
        if (bossController.beHit)
        {
            bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bTakeDameState);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
