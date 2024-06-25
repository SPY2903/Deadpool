using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : IState
{
    private PlayerController playerController;
    public JumpingState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        playerController.PlayerAnimator.CrossFade("Jumping  without space", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        playerController.Move();
        if (playerController.isCloseToGround)
        {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.fallState);
        }
        //if (playerController.PlayerVelocity.y <= -3f)
        //{
        //    playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.fallState);
        //}
        //if (playerController.IsClosingGround)
        //{
        //    playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.fallState);
        //}
        //if (playerController.checkCloseToGround.isCloseToGround)
        //{
        //    playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.fallState);
        //}
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
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
