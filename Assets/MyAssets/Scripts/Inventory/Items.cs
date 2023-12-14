using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    public bool equippable;
    public bool useable;
    public bool moveToHB;
    

    public List<TMP_Dropdown.OptionData> GetDropdownOptions()
    {
        // Logic to generate and return the dropdown options based on your object
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        if (equippable)
        {
            options.Add(new TMP_Dropdown.OptionData("Equip"));
        }
        if(useable)
        {
            options.Add(new TMP_Dropdown.OptionData("Use"));
        }
        if(moveToHB)
        {
            options.Add(new TMP_Dropdown.OptionData("Quickslot"));
        }

        options.Add(new TMP_Dropdown.OptionData("Destroy"));

        return options;
    }
}
