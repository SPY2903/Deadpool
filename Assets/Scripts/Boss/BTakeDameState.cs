using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTakeDameState : IState
{
    private BossController bossController;
    private bool isAnimatorPlaying;
    private bool isTakeDameAgain;
    public BTakeDameState(BossController bossController)
    {
        this.bossController = bossController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaying = true;
        bossController.Anim.CrossFade("Take Dame", .25f);
        bossController.TakeDame(bossController.dameTake);
        bossController.beHit = false;
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo state = bossController.Anim.GetCurrentAnimatorStateInfo(0);
        if (bossController.crtDetail.currentHealth == 0)
        {
            bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bDieState);
        }
        else
        {
            if (isAnimatorPlaying && state.normalizedTime >= .5f && !bossController.Anim.IsInTransition(0))
            {
                isAnimatorPlaying = false;
                if (bossController.crtDetail.currentHealth <= bossController.crtDetail.health / 2)
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bAngryState);
                }
                else
                {
                    if (bossController.CheckAtTargetPos())
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
                    else
                    {
                        bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bWalkState);
                    }
                }
                if (bossController.beHit)
                {
                    isTakeDameAgain = true;
                    bossController.beHit = false;
                }
            }
        }
        
        if (isTakeDameAgain)
        {
            isTakeDameAgain = false;
            isAnimatorPlaying = true;
            bossController.Anim.Play("Take Dame",0,0f);
            bossController.TakeDame(bossController.dameTake);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
