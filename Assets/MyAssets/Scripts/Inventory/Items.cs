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
    public string rarity;
    
    //public float durability;
    
    public bool isEquipped;
    public int numUses;
    public bool equippable;
    public int equipSlot;
    public bool useable;
    public bool moveToHB;

    public float maxHealth;
    public float maxMana;
    public float health;
    public float mana;

    public float expValue;
    public float damage;
    public float block;
    public bool bloackable;
    public float armor;   //mitigates damage
    public float vitality;  //good increase in HP  //?
    public float intelligence;  //increase in MP  //magic does more adamage  //skill tree for learning spells req higher intel?
    public float fortitude;     //reduces duration of stuns  //increases health slightly  //increased healing from spells and obj?
    public float strength;  //increase damage with swords and axes  //extra increase with blunt objects like hammers
    public float agility;   //influence attackspeed  //buff dash abilities? 
    

    public List<string> GetDropdownOptions()
    {
        // Logic to generate and return the dropdown options based on your object
        List<string> options = new List<string>();

        if (equippable)
        {
            options.Add("Equip");
        }
        if (useable)
        {
            options.Add("Use");
        }
        if (moveToHB)
        {
            options.Add("Quickslot");
        }

        options.Add("Destroy");

        return options;
    }

    public string GetDefault()
    {
        if(useable)
        {
            return "useable";
        }
        else if(equippable)
        {
            return "equippable";
        }
        else
        {
            return null;
        }
    }

    public void ResetHp()
    {
        health = maxHealth;
        mana = maxMana;
    }


    
}
