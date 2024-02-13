using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightLandingState : PlayerLandingState
{
    // Start is called before the first frame update
    public PlayerLightLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine) 
    {
    
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

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

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
    #endregion


}
