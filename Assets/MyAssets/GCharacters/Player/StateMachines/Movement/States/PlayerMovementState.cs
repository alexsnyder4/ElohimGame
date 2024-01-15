using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;

    protected Vector2 movementInput;

    protected float baseSpeed = 5f;

    protected float speedModifier = 1.0f;
    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        stateMachine = playerMovementStateMachine;
    }
    public virtual void Enter()
    {
        Debug.Log("State: " + GetType().Name);
    }

    public virtual void Exit()
    {
       
    }

    public virtual void HandleInput()
    {
        ReadMovementinput();
    }

    

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    

    public virtual void Update()
    {
        
    }

    #region Main Methods
    private void ReadMovementinput()
    {
        movementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }
    private void Move()
    {
        if(movementInput == Vector2.zero || speedModifier == 0f)
        {
            return;
        }

        Vector3 movementDirection = GetMovementDirection();

        float movementSpeed = GetMovementSpeed();

        Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

        stateMachine.Player.rb.AddForce(movementDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
    }

    


    #endregion

    #region Reusable Methods
    protected Vector3 GetMovementDirection()
    {
        return new Vector3(movementInput.x, 0f, movementInput.y);
    }
    protected float GetMovementSpeed()
    {
        return baseSpeed * speedModifier;
    }
    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = stateMachine.Player.rb.velocity;

        playerHorizontalVelocity.y = 0f;

        return playerHorizontalVelocity;
    }
    #endregion
}
