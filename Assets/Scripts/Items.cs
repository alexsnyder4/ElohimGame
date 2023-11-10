using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Items : ScriptableObject
{
    string itemName;
    string description;
    GameObject icon;
    string type;
    float damage;
    float durability;
    float defence;
}
