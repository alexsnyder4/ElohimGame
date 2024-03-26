using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatData
{
    [field: SerializeField] [field: Range(0f,5f)] public float AttackSpeed { get; private set; } = 1f;
    [field: SerializeField] public bool isInCombat = false;
}
