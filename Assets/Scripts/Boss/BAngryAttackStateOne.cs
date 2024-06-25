using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAngryAttackStateOne : IState
{
    private BossController bossController;
    private bool isAnimatorPlaying;
    public BAngryAttackStateOne(BossController bossController)
    {
        this.bossController = bossController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaying = true;
        bossController.Anim.CrossFade("Angry Attack 1", .25f);
        bossController.currentAttack = 1;
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo state = bossController.Anim.GetCurrentAnimatorStateInfo(0);
        if(isAnimatorPlaying && state.normalizedTime >= 1.0f && !bossController.Anim.IsInTransition(0))
        {
            isAnimatorPlaying = false;
            if (bossController.tgDetail.currentHealth == 0)
            {
                bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bFlexMuscleState);
            }
            else
            {
                if (bossController.CheckAtTargetPos())
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bAngryAttackStateTwo);
                }
                else
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bRunState);
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
