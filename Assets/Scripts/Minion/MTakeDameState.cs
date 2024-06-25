using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTakeDameState : IState
{
    MinionController minionController;
    private bool isAnimatorPlaying;
    private bool isTakeDameAgain;
    public MTakeDameState(MinionController minionController)
    {
        this.minionController = minionController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaying = true;
        minionController.Anim.CrossFade("Take dame", .15f);
        minionController.TakeDame(minionController.dameTake);
        minionController.beHit = false;
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo stateInfo = minionController.Anim.GetCurrentAnimatorStateInfo(0);

        if (minionController.crtDetail.currentHealth == 0)
        {
            minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mDieState);
        }
        if (isAnimatorPlaying && stateInfo.normalizedTime >= .4f && !minionController.Anim.IsInTransition(0))
        {
            isAnimatorPlaying = false;
            //if (minionController.findOutTarget)
            //{

            //}
            //else
            //{

            //}
            if (minionController.findOutTarget && !minionController.atTargetPos)
            {
                minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mRunState);
            }
            if (minionController.atTargetPos && !minionController.beHit)
            {
                minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mAttackState);
            }
            if (minionController.beHit)
            {
                isTakeDameAgain = true;
            }
        }
        if (isTakeDameAgain)
        {
            minionController.Anim.Play("Take dame", 0, 0f);
            isAnimatorPlaying = true;
            isTakeDameAgain = false;
            minionController.beHit = false;
            minionController.TakeDame(minionController.dameTake);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
        //minionController.Anim.Rebind();
    }
}
