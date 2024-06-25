using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIdleState : IState
{
    MinionController minionController;
    public MIdleState(MinionController minionController)
    {
        this.minionController = minionController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        minionController.Anim.CrossFade("Idle", .25f);
        minionController.atDefaultPos = false;
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        if (minionController.findOutTarget )
        {
            minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mRunState);
        }
        if (minionController.beHit)
        {
            minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mTakeDameState);
        }
        //if (minionController.atTargetPos && minionController.tgDetail.currentHealth != 0)
        //{
        //    minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mAttackState);
        //}
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
