using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerMovementState
{
    public PlayerAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();

        // Handle attacking input here
    }

    // Implement attacking logic here
}