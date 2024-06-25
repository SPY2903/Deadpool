using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryState : IState
{
    private PlayerController playerController;
    private bool isAnimatorPlaying;
    public RecoveryState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimatorPlaying = true;
        playerController.PlayerAnimator.CrossFade("Health Recover", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo state = playerController.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        if(isAnimatorPlaying && state.normalizedTime >= 1.0f && !playerController.PlayerAnimator.IsInTransition(0))
        {
            isAnimatorPlaying = false;
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.idleState);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
