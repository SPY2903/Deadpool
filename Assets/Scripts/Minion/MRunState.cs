using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRunState : IState
{
    MinionController minionController;
    public MRunState(MinionController minionController)
    {
        this.minionController = minionController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        minionController.Anim.CrossFade("Run", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        minionController.Move();
        if (minionController.atDefaultPos && !minionController.findOutTarget)
        {
            minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mIdleState);
        }
        if (minionController.beHit)
        {
            minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mTakeDameState);
        }
        if (minionController.atTargetPos && minionController.tgDetail.currentHealth != 0 && minionController.findOutTarget)
        {
            minionController.MstateMachine.TransitionTo(minionController.MstateMachine.mAttackState);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
