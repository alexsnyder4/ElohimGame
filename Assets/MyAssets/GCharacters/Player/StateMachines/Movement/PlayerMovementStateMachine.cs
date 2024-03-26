using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public GPlayer Player { get; }

    public PlayerStateReusableData ReusableData { get; }
    public PlayerIdlingState IdlingState { get; }
    public PlayerDashingState DashingState { get; }
    public PlayerWalkingState WalkingState { get; }
    public PlayerRunningState RunningState { get; }
    public PlayerSprintingState SprintingState { get; }

    public PlayerLightStoppingState LightStoppingState { get; }

    public PlayerMediumStoppingState MediumStoppingState { get; }

    public PlayerHardStoppingState HardStoppingState { get; }

    public PlayerLightLandingState LightLandingState { get; }

    public PlayerRollingState RollingState { get; }

    public PlayerHardLandingState HardLandingState { get; }

    public PlayerJumpingState JumpingState { get; }

    public PlayerFallingState FallingState { get; }

    //Combat States

    public PlayerBlockingState BlockingState { get; }

    public PlayerHeavyAttackingState HeavyAttackingState { get; }

    public PlayerLightAttackingState LightAttackingState { get; }

    public PlayerMovementStateMachine(GPlayer gPlayer)
    {
        Player = gPlayer;
        ReusableData = new PlayerStateReusableData();

        IdlingState = new PlayerIdlingState(this);
        DashingState = new PlayerDashingState(this);

        WalkingState = new PlayerWalkingState(this);
        RunningState = new PlayerRunningState(this);
        SprintingState = new PlayerSprintingState(this);

        LightStoppingState = new PlayerLightStoppingState(this);
        MediumStoppingState = new PlayerMediumStoppingState(this);
        HardStoppingState = new PlayerHardStoppingState(this);

        LightLandingState = new PlayerLightLandingState(this);
        RollingState = new PlayerRollingState(this);
        HardLandingState = new PlayerHardLandingState(this);    

        JumpingState = new PlayerJumpingState(this);
        FallingState = new PlayerFallingState(this);

        BlockingState = new PlayerBlockingState(this);
        LightAttackingState = new PlayerLightAttackingState(this);
        //HeavyAttackingState = new PlayerHeavyAttackingState(this);
    }
}
