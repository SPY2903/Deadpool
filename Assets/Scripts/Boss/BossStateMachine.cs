using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine
{
    public IState CurrentState { get; private set; }
    public BIdleState bIdleState;
    public BWalkState bWalkState;
    public BRunState bRunState;
    public BAttackStateOne bAttackStateOne;
    public BAttackStateTwo bAttackStateTwo;
    public BAngryState bAngryState;
    public BAngryAttackStateOne bAngryAttackStateOne;
    public BAngryAttackStateTwo bAngryAttackStateTwo;
    public BJumpAttackState bJumpAttackState;
    public BTakeDameState bTakeDameState;
    public BDieState bDieState;
    public BFlexMuscleState bFlexMuscleState;
    public BRoarState bRoarState;

    public BossStateMachine(BossController boss)
    {
        bIdleState = new BIdleState(boss);
        bWalkState = new BWalkState(boss);
        bRunState = new BRunState(boss);
        bAttackStateOne = new BAttackStateOne(boss);
        bAttackStateTwo = new BAttackStateTwo(boss);
        bAngryState = new BAngryState(boss);
        bAngryAttackStateOne = new BAngryAttackStateOne(boss);
        bAngryAttackStateTwo = new BAngryAttackStateTwo(boss);
        bJumpAttackState = new BJumpAttackState(boss);
        bTakeDameState = new BTakeDameState(boss);
        bDieState = new BDieState(boss);
        bFlexMuscleState = new BFlexMuscleState(boss);
        bRoarState = new BRoarState(boss);

    }

    public void Initilialize(IState startingState)
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
