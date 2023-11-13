using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;
    public Vector3 currPosition;
}
