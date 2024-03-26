using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLightAttackingState : PlayerMovementState
{
    public PlayerLightAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    private float startTime;
    private int ComboAttacksIndex;

    private bool shouldComboAttack;
   
    #region IState Methods
    public override void Enter()
    {
        base.Enter();


        StartAnimation(stateMachine.Player.AnimationData.LightAttackParameterHash);

        //UpdateConsecutiveDashes();

        startTime = Time.time;
    }
    public override void OnAnimationTransitionEvent()
    {
     
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.LightAttackParameterHash);

    }
    #endregion

    #region Reusable Methods
    /*
    protected override void AddInputActionsCallBacks()
    {
        base.AddInputActionsCallBacks();

        stateMachine.Player.Input.PlayerActions.Attack.performed += OnMovementPerformed;
    }

    

    protected override void RemoveInputActionsCallBacks()
    {
        base.RemoveInputActionsCallBacks();

        stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
    }
    */
    #endregion
    #region Main Methods
    
    /*
    private void UpdateConsecutiveDashes()
    {
        if (!IsConsecutive())
        {
            ConsecutiveDashesUsed = 0;
        }

        ++ConsecutiveDashesUsed;

        if(ConsecutiveDashesUsed == dashData.ConsecutiveDashesLimitAmount)
        {
            ConsecutiveDashesUsed = 0;

            stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash, dashData.DashLimitReachedCooldown);
        }


    }

    private bool IsConsecutive()
    {
        return Time.time < startTime + dashData.TimeToBeConsideredConsecutive;
    }
    */
    #endregion

    #region Input Methods


    #endregion
    /*
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.HardLandParameterHash);

        stateMachine.Player.Input.PlayerActions.Movement.Disable();

        resetVelocity();
    }
    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.HardLandParameterHash);

        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    }

    public override void OnAnimationExitEvent()
    {
        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    }
    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);

    }
    */
}
