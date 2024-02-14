using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMovingState
{

    private float startTime;

    private PlayerSprintData sprintData;
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        sprintData = movementData.SprintData;
    }

    #region IState Methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;

        base.Enter();

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.MediumForce;
        startTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if(!stateMachine.ReusableData.ShouldWalk)
        {
            return;
        }

        if(Time.time< startTime+ sprintData.RunToWalkTime)
        {
            return;
        }
        StopRunning();
    }


    #endregion

    #region Main Methods
    private void StopRunning()
    {
        if(stateMachine.ReusableData.MovementInput == Vector2.zero )
        {
            stateMachine.ChangeState(stateMachine.IdlingState); return;
        }

        stateMachine.ChangeState(stateMachine.WalkingState);
    }

    #endregion
    #region Reusable Methods
    protected override void AddInputActionsCallBacks()
    {
        base.AddInputActionsCallBacks();

        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
    }



    protected override void RemoveInputActionsCallBacks()
    {
        base.RemoveInputActionsCallBacks();

        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
    }
    #endregion


    #region Input Methods
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);

        stateMachine.ChangeState(stateMachine.WalkingState);
    }
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.MediumStoppingState);

        base.OnMovementCanceled(context);
    }

    #endregion
}
