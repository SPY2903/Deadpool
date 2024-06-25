using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDieState : IState
{
    private BossController bossController;
    private bool isAnimatorPlaying;
    public BDieState(BossController bossController)
    {
        this.bossController = bossController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaying = true;
        bossController.Anim.CrossFade("Die", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo state = bossController.Anim.GetCurrentAnimatorStateInfo(0);
        if(isAnimatorPlaying && state.normalizedTime >= 1.0f && !bossController.Anim.IsInTransition(0))
        {
            isAnimatorPlaying = false;
            bossController.isDie = true;
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
