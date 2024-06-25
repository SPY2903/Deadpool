using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSwordTwoState : IState
{
    private PlayerController playerController;
    private bool isAnimationPlaying;
    private bool wasClickPreviousFrame;
    public ComboSwordTwoState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimationPlaying = true;
        wasClickPreviousFrame = false;
        playerController.PlayerAnimator.CrossFade("Combo Sword 2", .25f);
        playerController.dpS.asSlash.Play();
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        if (Input.GetMouseButtonDown(0))
        {
            wasClickPreviousFrame = true;
        }
        AnimatorStateInfo stateInfo = playerController.PlayerAnimator.GetCurrentAnimatorStateInfo(0);

        if (isAnimationPlaying && stateInfo.normalizedTime >= 1.0f && !playerController.PlayerAnimator.IsInTransition(0))
        {
            isAnimationPlaying = false;
            if (wasClickPreviousFrame)
            {
                playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.comboSwordThreeState);
            }
            else
            {
                playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.idleState);
            }
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
