using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    private PlayerController playerController;
    private bool isAnimationPlaying;

    public JumpState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimationPlaying = true;
        //playerController.checkCloseToGround.isCloseToGround = false;
        playerController.PlayerAnimator.CrossFade("Jump without space", .25f);
        playerController.dpS.asJump.Play();
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        playerController.Move();
        AnimatorStateInfo stateInfo = playerController.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        if (isAnimationPlaying && stateInfo.normalizedTime >= 1.0f && !playerController.PlayerAnimator.IsInTransition(0))
        {
            isAnimationPlaying = false;
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.jumpingState);
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
    }
    public void Exit()
    {
        // code that runs when we exit the state
        playerController.dpS.asJump.Stop();
    }
}
