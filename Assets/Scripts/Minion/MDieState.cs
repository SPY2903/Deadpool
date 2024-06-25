using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDieState : IState
{
    MinionController minionController;
    private bool isAnimationPlaying;
    public MDieState(MinionController minionController)
    {
        this.minionController = minionController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimationPlaying = true;
        minionController.Anim.CrossFade("Die", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo state = minionController.Anim.GetCurrentAnimatorStateInfo(0);
        if(state.normalizedTime >= .5f)
        {
            minionController.HealthBar.SetActive(false);
        }
        if(isAnimationPlaying && state.normalizedTime >= 1.0f && !minionController.Anim.IsInTransition(0))
        {
            minionController.isDie = true;
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
