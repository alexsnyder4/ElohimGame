using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventTrigger : MonoBehaviour
{
    private GPlayer gPlayer;

    private void Awake()
    {
        gPlayer = transform.parent.GetComponent<GPlayer>();
    }

    private bool IsInAnimationTransition(int layerIndex = 0)
    {
        return gPlayer.anim.IsInTransition(layerIndex);
    }

    public void TriggerOnMovementStateAnimationEnterEvent()
    {
        if(IsInAnimationTransition())
        {
            return;
        }
        gPlayer.OnMovementStateAnimationEnterEvent();
    }
    public void TriggerOnMovementStateAnimationExitEvent()
    {
        if(IsInAnimationTransition())
        {
            return;
        }
        gPlayer.OnMovementStateAnimationExitEvent();
    }
    public void TriggerOnMovementStateAnimationTransitionEvent()
    {
        if(IsInAnimationTransition())
        {
            return;
        }
        gPlayer.OnMovementStateAnimationTransitionEvent();
    }
}
