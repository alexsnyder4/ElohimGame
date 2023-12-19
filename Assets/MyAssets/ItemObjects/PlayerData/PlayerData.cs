using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;
    public Vector3 currPosition;
    public float baseDamage;


    public float armor;   //mitigates damage
    public float vitality;  //good increase in HP  //?
    public float intelligence;  //increase in MP  //magic does more adamage  //skill tree for learning spells req higher intel?
    public float fortitude;     //reduces duration of stuns  //increases health slightly  //increased healing from spells and obj?
    public float strength;  //increase damage with swords and axes  //extra increase with blunt objects like hammers
    public float agility;   //influence attackspeed  //buff dash abilities? 

    public Items[] charKit = new Items[7];
    // 0 = Head
    // 1 = Chest
    // 2 = Legs
    // 3 = Gloves
    // 4 = Boots
    // 5 = RHand (primary)
    // 6 = LHand

    public void AddToStats(Items item)
    {
        //Equipment stats
        armor += item.armor;
        vitality += item.vitality;
        intelligence += item.intelligence;
        fortitude += item.fortitude;
        strength += item.strength;
        agility += item.agility;

        //Use only stats
        health += item.health;
        mana= item.mana;

    }
    public void RemoveFromStats(Items item)
    {
        armor -= item.armor;
        vitality -= item.vitality;
        intelligence -= item.intelligence;
        fortitude -= item.fortitude;
        strength -= item.strength;
        agility -= item.agility;
    }

    public void UpdateKit(Items[] kitArr)
    {
        for(int i = 0; i < kitArr.Length; i++)
        {
            charKit[i] = kitArr[i];
        }
    }

}
