using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public PlayerData playerData;
    public float currHealth;
    public float currMana;
    // Start is called before the first frame update
    void Start()
    {
        playerData.health = currHealth;
        playerData.mana = currMana;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
