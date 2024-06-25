using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MScreamState : IState
{
    private MinionController minionController;
    private bool isAnimatorPlaying;
    public MScreamState(MinionController minionController)
    {
        this.minionController = minionController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaying = true;
        minionController.Anim.CrossFade("Scream", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo state = minionController.Anim.GetCurrentAnimatorStateInfo(0);
        if(isAnimatorPlaying && state.normalizedTime >= 1.0f && !minionController.Anim.IsInTransition(0))
        {
            isAnimatorPlaying = false;
            minionController.isTargetDie = true;
            minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mRunState);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
