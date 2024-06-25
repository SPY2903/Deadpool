using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSwordThreeState : IState
{
    private PlayerController playerController;
    private bool isAnimationPlaying;
    public ComboSwordThreeState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimationPlaying = true;
        playerController.PlayerAnimator.CrossFade("Combo Sword 3", .25f);
        playerController.dpS.asSlash.Play();
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo stateInfo = playerController.PlayerAnimator.GetCurrentAnimatorStateInfo(0);

        if (isAnimationPlaying && stateInfo.normalizedTime >= 1.0f && !playerController.PlayerAnimator.IsInTransition(0))
        {
            isAnimationPlaying = false;
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.idleState);
        }
        if (Input.GetMouseButtonUp(1))
        {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.rollState);
        }
        //----------------------------------------------------------------------------------------------------------------
        // TAKE DAME
        if (playerController.CheckHit.beHit)
        {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.takeDameState);
        }
        //----------------------------------------------------------------------------------------------------------------
        // Recovery Health
        if (Input.GetKeyUp(KeyCode.H) && playerController.CrtDetail.currentHealth != playerController.CrtDetail.health)
        {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.recoveryState);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
        playerController.dpS.asSlash.Stop();
    }
}
