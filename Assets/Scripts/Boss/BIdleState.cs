using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIdleState : IState
{
    private BossController bossController;
    private bool isAnimatorPlaying;

    public BIdleState(BossController bossController)
    {
        this.bossController = bossController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaying = true;
        bossController.Anim.CrossFade("Idle", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo state = bossController.Anim.GetCurrentAnimatorStateInfo(0);
        if (isAnimatorPlaying && state.normalizedTime >= .5f && !bossController.Anim.IsInTransition(0))
        {
            isAnimatorPlaying = false;
            bossController.BStateMachine.TransitionTo(bossController.BStateMachine.bWalkState);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
