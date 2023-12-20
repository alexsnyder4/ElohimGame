using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditorInternal;
using Unity.VisualScripting;


public class ItemSelection : MonoBehaviour
{
    public List<Items> currInvList;
    public TMP_Text itemNameInInv;
    public Items itemToAction;
    public Button[] options = new Button[5];
    public bool isHotbarButton;
    public bool optionsExpanded;
    private bool hotbarSelectionActive = false;
    Color clear = new Color(1f, 1f, 1f, 0f);
    Color full = new Color(1f,1f,1f, 255f);
    

    void Start()
    {
        if(!isHotbarButton)
        {
            DisableButtons();
        }
        itemToAction = null;
        hotbarSelectionActive = false;
    }

    void Update()
    {
        // Check for keyboard input only if hotbar selection is active
        if (hotbarSelectionActive)
        {
            if(itemToAction == null)
            {
                currInvList = new List<Items>(InventoryManager.Instance.items);
                foreach(var inv in currInvList)
                {
                    if(inv.itemName == itemNameInInv.text.ToString())
                    {
                        itemToAction = inv;
                        break;
                    }
                }
            }
            
            Debug.Log("ActiveSelection");
            HandleHotbarSelectionInput();
        }
    }
    public void OnPointerEnter()
    {
        hotbarSelectionActive = true;
    }
    public void OnPointerExit()
    {
        hotbarSelectionActive = false;
    }




    public void HandleHotbarSelectionInput()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Hovering");
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Alpha1");
                EquipItemToHotbar(0);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                EquipItemToHotbar(1);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                EquipItemToHotbar(2);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                EquipItemToHotbar(3);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha5))
            {
                EquipItemToHotbar(4);
            }
        }
    }

    public void EquipItemToHotbar(int hotbarIndex)
    {
        if(InventoryManager.Instance.EquipItemToHotbarSlot(itemToAction, hotbarIndex))
        {
            InventoryManager.Instance.hotbarIcon[hotbarIndex].sprite = itemToAction.icon;
            InventoryManager.Instance.hotbarIcon[hotbarIndex].color = full;
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

    public void HotbarSelection(int num)
    {
        itemToAction = InventoryManager.Instance.hotbarItems[num];
        if(itemToAction == null)
        {
            Debug.Log("Null item to action");
            return;
        }
        string defaultAction = itemToAction.GetDefault();
        if(defaultAction == "useable")
        {
            InventoryManager.Instance.UseItem(itemToAction);
        }
        else if(defaultAction == "equippable")
        {
            InventoryManager.Instance.EquipItem(itemToAction);
            Debug.Log("Equipping");
        }
    }

}

