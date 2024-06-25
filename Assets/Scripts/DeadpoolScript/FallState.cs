using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState
{
    private PlayerController playerController;
    private bool isAnimationPlaying;

    public FallState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimationPlaying = true;
        playerController.PlayerAnimator.CrossFade("Fall without space", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        playerController.Move();
        AnimatorStateInfo stateInfo = playerController.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        
        if (isAnimationPlaying && stateInfo.normalizedTime >= .8f && !playerController.PlayerAnimator.IsInTransition(0))
        {
            isAnimationPlaying = false;
            //----------------------------------------------------------------------------------------------------------------
            // IDLE
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.idleState);
            }
            //----------------------------------------------------------------------------------------------------------------
            // WALK
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.walkState);
            }
            //----------------------------------------------------------------------------------------------------------------
            // RUN
            else if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && Input.GetKey(KeyCode.LeftShift))
            {
                playerController.SetCurrentSpeed(playerController.CrtDetail.speedUp);
                playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.runState);
            }
        }
        //----------------------------------------------------------------------------------------------------------------
        // TAKE DAME
        if (playerController.CheckHit.beHit)
        {
            playerController.TakeDame(playerController.CheckHit.dameTake);
            playerController.CheckHit.beHit = false;
            // DIE
            if (playerController.CrtDetail.currentHealth == 0)
            {
                playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.dieState);
            }
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
    }
}
