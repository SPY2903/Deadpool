using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordToBackState : IState
{
    private PlayerController playerController;
    private bool isAnimationPlaying;
    public SwordToBackState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimationPlaying = true;
        playerController.PlayerAnimator.CrossFade("Sword to back", .1f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo stateInfo = playerController.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        if (isAnimationPlaying && stateInfo.normalizedTime >= 1.0f && !playerController.PlayerAnimator.IsInTransition(0))
        {
            isAnimationPlaying = false;
            GameManager.Instance.isInPlayingMode = true;
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.idleState);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
