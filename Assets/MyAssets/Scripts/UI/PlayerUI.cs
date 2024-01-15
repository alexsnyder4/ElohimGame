using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    
    public PlayerData pd;
    public TMP_Text hp;
    public Image hpBar;
    public TMP_Text mana;
    public Image manaBar;
    public GameObject charPanel;
    public GameObject inventory;
    public bool invActive = false;
    public bool charActive = false;
    public Button inventoryBtn;
    public Button characterBtn;

    

    // Start is called before the first frame update
    void Start()
    {
        hp.text = pd.health + "/" + pd.maxHealth;
        mana.text = pd.mana + "/" + pd.maxMana;
        hpBar.fillAmount = pd.health/pd.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryBtn.onClick.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            characterBtn.onClick.Invoke();
        }
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
    public void UpdateHealthBar(float currentHp, float maxHp)
    {
        hp.text = currentHp + "/" + maxHp;
        mana.text = pd.mana + "/" + pd.maxMana;
        hpBar.fillAmount = currentHp/maxHp;
    }
}
