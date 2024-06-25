using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDameState : IState
{
    private PlayerController playerController;
    private bool isAnimationPlaying;
    public TakeDameState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        isAnimationPlaying = true;
        playerController.TakeDame(playerController.CheckHit.dameTake);
        playerController.PlayerAnimator.CrossFade("Take Dame", .25f);
        playerController.CheckHit.beHit = false;
        playerController.dpS.asTakeDame.Play();
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        AnimatorStateInfo stateInfo = playerController.PlayerAnimator.GetCurrentAnimatorStateInfo(0);

        if (isAnimationPlaying && stateInfo.normalizedTime >= .5f && !playerController.PlayerAnimator.IsInTransition(0))
        {
            isAnimationPlaying = false;
            //----------------------------------------------------------------------------------------------------------------
            // DIE
            if (playerController.CrtDetail.currentHealth == 0)
            {
                playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.dieState);
            }
            else
            {
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
                //----------------------------------------------------------------------------------------------------------------
                // HAND ATTACH 1
                if (Input.GetMouseButtonUp(0) && GameManager.Instance.isInPlayingMode && GameManager.Instance.currentMode.Equals("Punch Mode"))
                {
                    playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.comboHandOneState);
                }
                //----------------------------------------------------------------------------------------------------------------
                // SWORD ATTACH 1
                else if (Input.GetMouseButtonUp(0) && GameManager.Instance.isInPlayingMode && GameManager.Instance.currentMode.Equals("Sword Mode"))
                {
                    playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.comboSwordOneState);
                }
                //----------------------------------------------------------------------------------------------------------------
                // ROLL
                if (Input.GetMouseButtonUp(1))
                {
                    playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.rollState);
                }
                //----------------------------------------------------------------------------------------------------------------
                // JUMP
                if (Input.GetButtonDown("Jump") && playerController.PlayerVelocity.y == 0)
                {
                    playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.jumpState);
                }
            }
        }

    }
    public void Exit()
    {
        // code that runs when we exit the state
        playerController.dpS.asTakeDame.Stop();
    }
}
