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
    

    void Start()
    {

    }

    public void ShowContextMenu()
    {
        Debug.Log("Showing menu");
        currInvList = new List<Items>(InventoryManager.Instance.items);
        // Show the context menu at the button's position

        Debug.Log(itemNameInInv.text.ToString());

        foreach(var inv in currInvList)
        {
            Debug.Log(inv.itemName);
            if(inv.itemName == itemNameInInv.text.ToString())
            {
                itemToAction = inv;
                break;
            }
            
        }
        // Use the associated scriptable object to generate dropdown options
    }
    public void Equip()
    {
        InventoryManager.Instance.EquipItem(itemToAction);
    }

    public void Destroy()
    {

    }

    public void Use()
    {

    }

    public void Inspect()
    {
        
    }
}

