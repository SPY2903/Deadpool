using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    private PlayerController playerController;

    public WalkState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        // code that runs when we first enter the state
        playerController.SetCurrentSpeed(playerController.CrtDetail.speed);
        playerController.PlayerAnimator.CrossFade("Walk", .25f);
        playerController.dpS.asWalk.Play();
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        //----------------------------------------------------------------------------------------------------------------
        // CHANGE ATTACK MODE
        if (!GameManager.Instance.isInPlayingMode && GameManager.Instance.isAttackModeChange && (Input.GetKeyUp(KeyCode.Return) || GameManager.Instance.isClickToChangeAttackMode))
        {
            if (GameManager.Instance.currentMode.Equals("Punch Mode"))
            {
                playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.swordToBackState);
            }
            else if (GameManager.Instance.currentMode.Equals("Sword Mode"))
            {
                playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.swordToHandState);
            }
            GameManager.Instance.isAttackModeChange = false;
            GameManager.Instance.isClickToChangeAttackMode = false;
        }
        //----------------------------------------------------------------------------------------------------------------
        // IDLE
        playerController.Move();
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.idleState);
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
        if (Input.GetMouseButtonUp(1) && GameManager.Instance.isInPlayingMode)
        {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.rollState);
        }
        //----------------------------------------------------------------------------------------------------------------
        // JUMP
        if (Input.GetButtonDown("Jump") && playerController.GroundPlayer && GameManager.Instance.isInPlayingMode)
        {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.jumpState);
        }
        //----------------------------------------------------------------------------------------------------------------
        // FALLING
        if (!playerController.GroundPlayer && playerController.PlayerVelocity.y < 0)
        {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.jumpingState);
            playerController.isCloseToGround = false;
        }
        //----------------------------------------------------------------------------------------------------------------
        // TAKE DAME
        if (playerController.CheckHit.beHit)
        {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.takeDameState);
        }
        //----------------------------------------------------------------------------------------------------------------
        // Recovery Health
        if (Input.GetKeyUp(KeyCode.H) && playerController.CrtDetail.currentHealth != playerController.CrtDetail.health && GameManager.Instance.isInPlayingMode)
        {
            playerController.PlayerStateMachine.TransitionTo(playerController.PlayerStateMachine.recoveryState);
        }
    }
    public void Exit()
    {
        // code that runs when we exit the state
        playerController.dpS.asWalk.Stop();
    }
}
