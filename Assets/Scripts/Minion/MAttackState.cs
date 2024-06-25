using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAttackState : IState
{
    MinionController minionController;
    private bool isAnimatorPlaying;
    private bool isPlayAgain;
    public MAttackState(MinionController minionController)
    {
        this.minionController = minionController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaying = true;
        minionController.Anim.CrossFade("Attack", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        minionController.FaceToTarget();
        AnimatorStateInfo state = minionController.Anim.GetCurrentAnimatorStateInfo(0);
        if(isAnimatorPlaying && state.normalizedTime >= 1.0f && !minionController.Anim.IsInTransition(0))
        {
            isAnimatorPlaying = false;
            if (minionController.CheckAtTargetPos() && minionController.tgDetail.currentHealth != 0)
            {
                isPlayAgain = true;
            }
            //Debug.Log(minionController.CheckAtTargetPos() + "-" + minionController.findOutTarget);
            if (!minionController.CheckAtTargetPos() || !minionController.findOutTarget)
            {
                minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mRunState);
            }
            if (minionController.tgDetail.currentHealth == 0)
            {
                minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mScreamState);
                //isPlayAgain = false;
            }
        }
        //Debug.Log(isPlayAgain);
        if (isPlayAgain)
        {
            minionController.Anim.Play("Attack", 0, 0f);
            isAnimatorPlaying = true;
            isPlayAgain = false;
        }
        if (minionController.beHit)
        {
            minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mTakeDameState);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
