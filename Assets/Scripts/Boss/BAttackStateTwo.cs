using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAttackStateTwo : IState
{
    private BossController bossController;
    private bool isAnimatorPlaing;

    public BAttackStateTwo(BossController bossController)
    {
        this.bossController = bossController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaing = true;
        bossController.Anim.CrossFade("Normal Attack 2", .25f);
        bossController.currentAttack = 2;
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo state = bossController.Anim.GetCurrentAnimatorStateInfo(0);
        if(isAnimatorPlaing && state.normalizedTime >= 1.0f && !bossController.Anim.IsInTransition(0))
        {
            isAnimatorPlaing = false;
            if (bossController.tgDetail.currentHealth == 0)
            {
                bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bFlexMuscleState);
            }
            else
            {
                if (bossController.CheckAtTargetPos())
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bJumpAttackState);
                }
                else
                {
                    bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bWalkState);
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
