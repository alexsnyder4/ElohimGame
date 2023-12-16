using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Items> items = new();
    public PlayerData pd;
    public Transform ItemContent;
    public GameObject InventoryItem;
    public Items[] hotbar = new Items[5];

    [SerializeField]
    public Items[] playerEquippedItems = new Items[7];
    // 0 = Head
    // 1 = Chest
    // 2 = Legs
    // 3 = Gloves
    // 4 = Boots
    // 5 = RHand (primary)
    // 6 = LHand



    private void Awake()
    {
        Instance = this;
    }

    public void Add(Items item)
    {
        items.Add(item);
    }
    public void Remove(Items item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        foreach(Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach(var item in items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("Icon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }



    public void EquipItem(Items item)
    {

        // Check the equipSlot value to determine where to place the item in the array
        int equipSlot = item.equipSlot;

        // Ensure the equipSlot is within the valid range
        if (equipSlot >= 0 && equipSlot < playerEquippedItems.Length)
        {
            // Equip the item in the appropriate slot
            if(playerEquippedItems[equipSlot] == null)
            {
                playerEquippedItems[equipSlot] = item;
                pd.AddToStats(item);
            }
            else
            {
                UnequipItem(playerEquippedItems[equipSlot]);
                playerEquippedItems[equipSlot] = item;
                pd.AddToStats(item);
            }
            if(item.type == "Weapon")
            {
                hotbar[0] = item;
            }
            else if(item.type == "offhand")
            {
                hotbar[1] = item;
            }
        }
        else
        {
            Debug.LogError("Invalid equipSlot value: " + equipSlot);
        }
        pd.UpdateKit(playerEquippedItems);
    }

    public void UnequipItem(Items item)
    {
        pd.RemoveFromStats(playerEquippedItems[item.equipSlot]);
        playerEquippedItems[item.equipSlot] = null;
        if(item.type == "Weapon")
        {
            hotbar[0] = null;
        }
        else if(item.type == "offhand")
        {
            hotbar[1] = null;
        }
    }
    
}

