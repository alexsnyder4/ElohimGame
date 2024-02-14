using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdlingState : PlayerGroundedState
{
    private PlayerIdleData idleData;
    public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        idleData = movementData.IdleData;
    }

    #region IState Methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        stateMachine.ReusableData.BackwardsCameraRecenteringData = idleData.BackwardsCameraRecenteringData;
        
        base.Enter();

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StationaryForce;
        
        resetVelocity();
    }

    public override void Update()
    {
        base.Update();

        if(stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            return;
        }

        OnMove();
    }

    
    #endregion
}
