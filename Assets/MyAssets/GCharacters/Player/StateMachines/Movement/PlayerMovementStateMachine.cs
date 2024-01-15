using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public GPlayer Player { get; }
    public PlayerIdlingState IdlingState { get; }
    public PlayerWalkingState WalkingState { get; }
    public PlayerRunningState RunningState { get; }
    public PlayerSprintingState SprintingState { get; }


    public PlayerMovementStateMachine(GPlayer gPlayer)
    {
        Player = gPlayer;
        IdlingState = new PlayerIdlingState(this);

        WalkingState = new PlayerWalkingState(this);
        RunningState = new PlayerRunningState(this);
        SprintingState = new PlayerSprintingState(this);
    }
}
