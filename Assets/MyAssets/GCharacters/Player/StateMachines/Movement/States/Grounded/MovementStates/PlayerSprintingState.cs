using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprintingState : PlayerMovingState
{
    private PlayerSprintData sprintData;

    private float startTime;

    private bool keepSprinting;

    private bool shouldResetSprintState;
    public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        sprintData = movementData.SprintData;
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModifier;

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

        shouldResetSprintState = true;
        startTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if(keepSprinting)
        {
            return;
        }

        if(Time.time < startTime + sprintData.SprintToRunTime)
        {
            return;
        }

        StopSprinting();

    }

    public override void Exit()
    {
        base.Exit();
        if(shouldResetSprintState)
        {
            keepSprinting = false;
            stateMachine.ReusableData.ShouldSprint = false;
        }
        

        
    }

    #endregion

    #region Main Methods
    private void StopSprinting()
    {
        if(stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdlingState); return;
        }

        stateMachine.ChangeState(stateMachine.RunningState);
    }
    #endregion

    #region Reusable Methods

    protected override void AddInputActionsCallBacks()
    {

        stateMachine.Player.Input.PlayerActions.Sprint.performed += OnSprintPerformed;
    }

    

    protected override void RemoveInputActionsCallBacks()
    {
        base.RemoveInputActionsCallBacks();
    }

    protected override void OnFall()
    {
        shouldResetSprintState = false;
        
        base.OnFall();
    }
    #endregion

    #region Input Methods
    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        keepSprinting = true;

        stateMachine.ReusableData.ShouldSprint = true;
    }
    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        shouldResetSprintState = false;
        base.OnJumpStarted(context);

    }
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.HardStoppingState);
    }
    #endregion
}
