using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditorInternal;


public class ItemSelection : MonoBehaviour
{
    public Button itemButton;
    public List<Items> currInvList;
    public TMP_Text itemNameInInv;
    public Items itemToAction;
    public Button[] options = new Button[4];
    public bool optionsExpanded;
    
    

    void Start()
    {
        DisableButtons();
    }

    public void ShowContextMenu()
    {
        if(InventoryManager.Instance.optionsExpanded)
        {
            InventoryManager.Instance.CloseOptions();
        }
        InventoryManager.Instance.optionsExpanded = true;
        EnableButtons();
        currInvList = new List<Items>(InventoryManager.Instance.items);
        foreach(var inv in currInvList)
        {
            Debug.Log(inv.itemName);
            if(inv.itemName == itemNameInInv.text.ToString())
            {
                itemToAction = inv;
                break;
            }
            
        }
    }
    public void Equip()
    {
        InventoryManager.Instance.EquipItem(itemToAction);
        DisableButtons();
    }

    public void Destroy()
    {
        DisableButtons();
    }

    public void Use()
    {
        DisableButtons();
    }

    public void Inspect()
    {
        DisableButtons();     
    }
    public void EnableButtons()
    {
        foreach(Button btn in options)
        {
            
            btn.gameObject.SetActive(true);
        }
    }
    public void DisableButtons()
    {
        foreach(Button btn in options)
        {
            InventoryManager.Instance.optionsExpanded = false;
            btn.gameObject.SetActive(false);
        }
    }
}

