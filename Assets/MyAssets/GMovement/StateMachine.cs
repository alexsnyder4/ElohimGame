using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    protected IState currentState;

    //Return string of current state player is in, if player is not in a state it will say "No State"
    public string CurrentStateName => currentState != null ? currentState.GetType().Name : "No State";

    public void ChangeState(IState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }

    public void OnAnimationEnterEvent()
    {
        currentState?.OnAnimationEnterEvent();
    }

    public void OnAnimationExitEvent()
    {
        currentState?.OnAnimationExitEvent();
    }

    public void OnAnimationTransitionEvent()
    {
        currentState?.OnAnimationTransitionEvent();
    }

    public void OnTriggerEnter(Collider other)
    {
        currentState?.OnTriggerEnter(other);
    }

    public void OnTriggerExit(Collider other)
    {
        currentState?.OnTriggerExit(other);
    }
}
