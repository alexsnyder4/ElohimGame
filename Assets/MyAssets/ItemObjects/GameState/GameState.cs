using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameState", menuName = "Game/GameState")]
public class GameState : ScriptableObject
{
    // Define your game state variables
    public PlayerData playerData;
    public string playerName;
    public float currHealth = 100;
    public float maxHP = 100;
    public float currMana = 50;
    public float maxMana = 50;

    public void Initialize()
    {
        playerData.health = currHealth;
        playerData.maxHealth = maxHP;
        playerData.mana = currMana;
        playerData.maxMana = maxMana;
        playerData.baseDamage = 2;


        playerData.armor = 1;   //mitigates damage
        playerData.vitality = 10;  //good increase in HP  //?
        playerData.intelligence = 5;  //increase in MP  //magic does more adamage  //skill tree for learning spells req higher intel?
        playerData.fortitude = 5;     //reduces duration of stuns  //increases health slightly  //increased healing from spells and obj?
        playerData.strength = 5;  //increase damage with swords and axes  //extra increase with blunt objects like hammers
        playerData.agility = 5;   //influence attackspeed  //buff dash abilities? 

    }
}
