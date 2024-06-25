using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : IState
{
    private PlayerController playerController;
    private bool isAnimationPlaying;

    public RollState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimationPlaying = true;
        playerController.PlayerAnimator.CrossFade("Roll Forward", .25f);
        playerController.dpS.asRoll.Play();
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        playerController.Gravity();
        AnimatorStateInfo stateInfo = playerController.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        if (isAnimationPlaying && stateInfo.normalizedTime >= 1.0f && !playerController.PlayerAnimator.IsInTransition(0))
        {
            isAnimationPlaying = false;

            //----------------------------------------------------------------------------------------------------------------
            // JUMP
            if (Input.GetButtonDown("Jump") && playerController.PlayerVelocity.y == 0)
            {
                playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.jumpState);
            }
            //----------------------------------------------------------------------------------------------------------------
            // IDLE
            else
            {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.idleState);
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
    }
    public void Exit()
    {
        // code that runs when we exit the state
        playerController.dpS.asRoll.Stop();
    }
}
