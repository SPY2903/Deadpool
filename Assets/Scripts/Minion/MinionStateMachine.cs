using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStateMachine
{
    public IState CurrentState { get; private set; }
    public MIdleState mIdleState;
    public MRunState mRunState;
    public MAttackState mAttackState;
    public MTakeDameState mTakeDameState;
    public MDieState mDieState;
    public MScreamState mScreamState;
    public MinionStateMachine(MinionController minion)
    {
        this.mIdleState = new MIdleState(minion);
        this.mRunState = new MRunState(minion);
        this.mAttackState = new MAttackState(minion);
        this.mTakeDameState = new MTakeDameState(minion);
        this.mDieState = new MDieState(minion);
        this.mScreamState = new MScreamState(minion);
    }
    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}
