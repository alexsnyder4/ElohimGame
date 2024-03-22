using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatData : MonoBehaviour
{
    [field: SerializeField] [field: Range(0f,5f)] public float AttackSpeed { get; private set; } = 1f;
}
