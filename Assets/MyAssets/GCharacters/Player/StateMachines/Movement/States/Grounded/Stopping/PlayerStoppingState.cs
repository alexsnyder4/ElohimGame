using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStoppingState : PlayerGroundedState
{
    public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }



    #region IState Methods

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;
        
        SetBaseCameraRecenteringData();
        
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        RotateTowardsTargetRotation();

        if (!IsMovingHorizontally())
        {
            return;
        }

        DecelerateHorizontally();
    }
    public override void OnAnimationTransitionEvent()
    {


        stateMachine.ChangeState(stateMachine.IdlingState);
    }


    #endregion

    #region Resuable Methods

    protected override void AddInputActionsCallBacks()
    {
        base.AddInputActionsCallBacks();

        stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
    }

    

    protected override void RemoveInputActionsCallBacks()
    {
        base.RemoveInputActionsCallBacks();

        stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
    }

    #endregion

    #region Input Methods

    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        OnMove();
    }
    #endregion
}
