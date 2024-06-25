using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAngryState : IState
{
    private BossController bossController;
    private bool isAnimatorPlaying;
    public BAngryState(BossController bossController)
    {
        this.bossController = bossController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaying = true;
        bossController.Anim.CrossFade("Angry", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo state = bossController.Anim.GetCurrentAnimatorStateInfo(0);
        if(isAnimatorPlaying && state.normalizedTime >= 1.0f && !bossController.Anim.IsInTransition(0))
        {
            isAnimatorPlaying = false;
            if (bossController.CheckAtTargetPos())
            {
                if (bossController.currentAttack == 0)
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bAngryAttackStateOne);
                }
                if (bossController.currentAttack == 1)
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bAngryAttackStateTwo);
                }
                if (bossController.currentAttack == 2)
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bJumpAttackState);
                }
            }
            else
            {
                bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bRunState);
            }
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
