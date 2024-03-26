using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [Header("State GroupPartner Names")]
    [SerializeField] private string groundedParameterName = "Grounded";
    [SerializeField] private string movingParameterName = "Moving";
    [SerializeField] private string stoppingParameterName = "Stopping";
    [SerializeField] private string landingParameterName = "Landing";
    [SerializeField] private string airborneParameterName = "Airborne";

    [Header("Grounded Parameter Names")]
    [SerializeField] private string idleParameterName = "isIdling";
    [SerializeField] private string dashParameterName = "isDashing";
    [SerializeField] private string walkParameterName = "isWalking";
    [SerializeField] private string runParameterName = "isRunning";
    [SerializeField] private string sprintParameterName = "isSprinting";
    [SerializeField] private string runStopParameterName = "isRunStopping";
    [SerializeField] private string dashStopParameterName = "isDashStopping";
    [SerializeField] private string rollParameterName = "isRolling";
    [SerializeField] private string hardLandParameterName = "isHardLanding";

    [Header("Airborne Parameter Names")]
    [SerializeField] private string fallParameterName = "isFalling";

    [Header("Combat Parameter Names")]
    [SerializeField] private string lightAttackParameterName = "isLightAttacking";
    [SerializeField] private string heavyAttackParameterName = "isHeavyAttacking";
    [SerializeField] private string blockParameterName = "isBlocking";


    public int GroundedParameterHash { get; private set; }
    public int MovingParameterHash { get; private set; }
    public int StoppingParameterHash { get; private set; }
    public int LandingParameterHash { get; private set; }
    public int AirborneParameterHash { get; private set; }

    public int IdleParameterHash { get; private set; }
    public int DashParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int SprintParameterHash { get; private set; }
    public int RunStopParameterHash { get; private set; }
    public int DashStopParameterHash { get; private set; }
    public int RollParameterHash { get; private set; }
    public int HardLandParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }

    public int LightAttackParameterHash { get; private set; }
    public int HeavyAttackParameterHash { get; private set; }
    public int BlockingParameterHash { get; private set; }


    public void Initialize()
    {
        GroundedParameterHash = Animator.StringToHash(groundedParameterName);
        MovingParameterHash = Animator.StringToHash(movingParameterName);
        StoppingParameterHash = Animator.StringToHash(stoppingParameterName);
        LandingParameterHash = Animator.StringToHash(landingParameterName);
        AirborneParameterHash = Animator.StringToHash(airborneParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        DashParameterHash = Animator.StringToHash(dashParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        SprintParameterHash = Animator.StringToHash(sprintParameterName);
        RunStopParameterHash = Animator.StringToHash(runStopParameterName);
        DashStopParameterHash = Animator.StringToHash(dashStopParameterName);
        RollParameterHash = Animator.StringToHash(rollParameterName);
        HardLandParameterHash = Animator.StringToHash(hardLandParameterName);
        FallParameterHash = Animator.StringToHash(fallParameterName);
        LightAttackParameterHash = Animator.StringToHash(lightAttackParameterName);
        HeavyAttackParameterHash = Animator.StringToHash(heavyAttackParameterName);
        BlockingParameterHash = Animator.StringToHash(blockParameterName);
    }
}
