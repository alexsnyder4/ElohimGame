using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "New Item")]
public class Items : ScriptableObject
{
    public string itemName;
    public string description;
    public GameObject prefab;
    public Sprite icon;
    public string type;
    public float damage;
    public float durability;
    public float defence;
}
