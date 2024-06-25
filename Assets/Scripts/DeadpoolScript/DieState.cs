using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    private PlayerController playerController;
    public DieState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public void Enter()
    {
        // code that runs when we first enter the state
        playerController.PlayerAnimator.CrossFade("Die", .25f);
    }
    public void Update()
    {
        // per-frame logic, include condition to transition to a new state
        playerController.Gravity();
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
