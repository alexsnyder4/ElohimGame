using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    
    public PlayerData pd;
    public TMP_Text hp;
    public TMP_Text mana;
    public GameObject charPanel;
    public GameObject inventory;
    public bool invActive = false;
    public bool charActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp.text = pd.health + "/" + pd.maxHealth;
        mana.text = pd.mana + "/" + pd.maxMana;
    }
    public void ToggleClickInventory()
    {
        inventory.SetActive(!invActive);
        invActive = !invActive;
    }
    public void ToggleClickCharPanel()
    {
        charPanel.SetActive(!charActive);
        charActive = !charActive;
    }
    
}
