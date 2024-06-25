using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public IState CurrentState { get; private set; }
    public IdleState idleState;
    public WalkState walkState;
    public RunState runState;
    public JumpState jumpState;
    public JumpingState jumpingState;
    public FallState fallState;
    public FallAttackState fallAttackState;
    public RollState rollState;
    public ComboHandOneState comboHandOneState;
    public ComboHandTwoState comboHandTwoState;
    public ComboHandThreeState comboHandThreeState;
    public ComboSwordOneState comboSwordOneState;
    public ComboSwordTwoState comboSwordTwoState;
    public ComboSwordThreeState comboSwordThreeState;
    public TakeDameState takeDameState;
    public DieState dieState;
    public SwordToHandState swordToHandState;
    public SwordToBackState swordToBackState;
    public RecoveryState recoveryState;
    public StateMachine(PlayerController player)
    {
        this.idleState = new IdleState(player);
        this.walkState = new WalkState(player);
        this.runState = new RunState(player);
        this.jumpState = new JumpState(player);
        this.jumpingState = new JumpingState(player);
        this.fallState = new FallState(player);
        this.fallAttackState = new FallAttackState(player);
        this.rollState = new RollState(player);
        this.comboHandOneState = new ComboHandOneState(player);
        this.comboHandTwoState = new ComboHandTwoState(player);
        this.comboHandThreeState = new ComboHandThreeState(player);
        this.comboSwordOneState = new ComboSwordOneState(player);
        this.comboSwordTwoState = new ComboSwordTwoState(player);
        this.comboSwordThreeState = new ComboSwordThreeState(player);
        this.takeDameState = new TakeDameState(player);
        this.dieState = new DieState(player);
        this.swordToHandState = new SwordToHandState(player);
        this.swordToBackState = new SwordToBackState(player);
        this.recoveryState = new RecoveryState(player);
    }
    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }
    public void TransitionTo(IState nextState)
    {
        if (GameManager.Instance.canChangeState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();
        }
    }
    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}
