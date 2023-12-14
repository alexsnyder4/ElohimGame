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
    public TMP_Dropdown dropdown;
    public List<Items> currInvList;
    public TMP_Text itemNameInInv;

    void Start()
    {

    }

    public void ShowContextMenu()
    {
        Debug.Log("Showing menu");
        currInvList = new List<Items>(InventoryManager.Instance.items);
        // Show the context menu at the button's position

        dropdown.transform.position = itemButton.transform.position;

        // Clear existing options in the dropdown
        dropdown.ClearOptions();
        Debug.Log(itemNameInInv.text.ToString());

        foreach(var inv in currInvList)
        {
            Debug.Log(inv.itemName);
            if(inv.itemName == itemNameInInv.text.ToString())
            {
                dropdown.AddOptions(inv.GetDropdownOptions());
            }
        }
        // Use the associated scriptable object to generate dropdown options
        dropdown.Show();
    }
}

