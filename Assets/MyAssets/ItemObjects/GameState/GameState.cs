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
    }
}
