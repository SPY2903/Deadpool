using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BJumpAttackState : IState
{
    private BossController bossController;
    private bool isAnimatorPlaying;

    public BJumpAttackState(BossController bossController)
    {
        this.bossController = bossController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaying = true;
        bossController.Anim.CrossFade("Jump Attack", .25f);
        bossController.currentAttack = 0;
        bossController.navMesh.speed = 400;
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        bossController.Jump(bossController.Target);
        AnimatorStateInfo state = bossController.Anim.GetCurrentAnimatorStateInfo(0);
        if (isAnimatorPlaying && state.normalizedTime >= 1.0f && !bossController.Anim.IsInTransition(0))
        {
            isAnimatorPlaying = false;
            if (bossController.tgDetail.currentHealth == 0)
            {
                bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bFlexMuscleState);
            }
            else
            {
                if(bossController.crtDetail.currentHealth <= bossController.crtDetail.health / 2)
                {
                    if (bossController.CheckAtTargetPos())
                    {
                        bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bAngryAttackStateOne);
                    }
                    else
                    {
                        bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bRunState);
                    }
                    bossController.navMesh.speed = bossController.crtDetail.speedUp;
                }
                else
                {
                    if (bossController.CheckAtTargetPos())
                    {
                        bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bAttackStateOne);
                    }
                    else
                    {
                        bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bWalkState);
                    }
                    bossController.navMesh.speed = bossController.crtDetail.speed;
                }
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
