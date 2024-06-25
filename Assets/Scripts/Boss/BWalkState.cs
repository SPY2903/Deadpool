using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BWalkState : IState
{
    private BossController bossController;

    public BWalkState(BossController bossController)
    {
        this.bossController = bossController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        bossController.Anim.CrossFade("Walk", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        bossController.Move(bossController.Target);
        if (bossController.tgDetail.currentHealth == 0)
        {
            bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bFlexMuscleState);
        }
        else
        {
            if (bossController.isAtTarget && !bossController.beHit)
            {
                if (bossController.currentAttack == 0)
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bAttackStateOne);
                }
                else if (bossController.currentAttack == 1)
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bAttackStateTwo);
                }
                else if (bossController.currentAttack == 2)
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bJumpAttackState);
                }
            }
            if(!bossController.isAtTarget && bossController.crtDetail.currentHealth <= bossController.crtDetail.health / 2)
            {
                bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bRunState);
            }
            if (bossController.beHit)
            {
                bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bTakeDameState);
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
