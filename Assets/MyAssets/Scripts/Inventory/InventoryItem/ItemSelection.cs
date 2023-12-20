using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditorInternal;


public class ItemSelection : MonoBehaviour
{
    public List<Items> currInvList;
    public TMP_Text itemNameInInv;
    public Items itemToAction;
    public Button[] options = new Button[5];
    public bool optionsExpanded;
    public InventoryManager inventoryManager;
    
    

    void Start()
    {
        DisableButtons();
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
    void Update()
    {
        // Check if the mouse is over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Handle keyboard input for hotbar selection
            HandleHotbarSelectionInput();
        }
    }

   void HandleHotbarSelectionInput()
    {
        // Check if any of the number keys (1-5) are pressed
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                // Equip the item to the corresponding hotbar slot
              //  EquipItemToHotbar(i);
            }
        }
    }


    public void ShowContextMenu()
    {
        if(InventoryManager.Instance.optionsExpanded)
        {
            InventoryManager.Instance.CloseOptions();
        }
        InventoryManager.Instance.optionsExpanded = true;
        EnableButtons();
        var currInvList = new List<Items>(InventoryManager.Instance.items);
        foreach(var inv in currInvList)
        {
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
        InventoryManager.Instance.DestroyItem(itemToAction);
        DisableButtons();
    }

    public void Use()
    {
        InventoryManager.Instance.UseItem(itemToAction);
        itemToAction.numUses--;
        if(itemToAction.numUses <= 0)
        {
            InventoryManager.Instance.DestroyItem(itemToAction);
        }
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

    public void OnButtonClick()
    {
        var currInvList = new List<Items>(InventoryManager.Instance.items);
        foreach(var inv in currInvList)
        {
            if(inv.itemName == itemNameInInv.text.ToString())
            {
                itemToAction = inv;
                break;
            }
            
        }
        string defaultAction = itemToAction.GetDefault();
        if(defaultAction == "useable")
        {
            InventoryManager.Instance.UseItem(itemToAction);
        }
        else if(defaultAction == "equippable")
        {
            InventoryManager.Instance.EquipItem(itemToAction);
        }
    }

}

