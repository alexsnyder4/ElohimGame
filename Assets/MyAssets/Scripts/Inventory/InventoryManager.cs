using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public bool optionsExpanded;
    public List<Items> items = new();
    public PlayerData pd;
    public Transform ItemContent;
    public GameObject InventoryItem;
    public CharacterPanelScript charPanel;


    public Items[] hotbarItems = new Items[5];
    public Image[] hotbarIcon = new Image[5];


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

    private void Start()
    {
        optionsExpanded = false;
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
                item.isEquipped = true;
            }
            else
            {
                UnequipItem(playerEquippedItems[equipSlot]);
                playerEquippedItems[equipSlot] = item;
                pd.AddToStats(item);
                item.isEquipped = true;
            }
        }
        else
        {
            Debug.LogError("Invalid equipSlot value: " + equipSlot);
        }
        
        pd.UpdateKit(playerEquippedItems);
        charPanel.UpdateCharacterLoadout();
    }

    public void UnequipItem(Items item)
    {
        pd.RemoveFromStats(playerEquippedItems[item.equipSlot]);
        item.isEquipped = false;
        playerEquippedItems[item.equipSlot] = null;
    
        pd.UpdateKit(playerEquippedItems);
    }
    public void CloseOptions()
    {
        foreach(Transform item in ItemContent)
        {
            item.gameObject.GetComponent<ItemSelection>().DisableButtons();
        }
    }

    public void UseItem(Items item)
    {
        pd.AddToStats(item);
    }
    
    public void DestroyItem(Items item)
    {
        // Remove the item from the items list
        items.Remove(item);

        // Check if the item is equipped and unequip it
        if(item.isEquipped)
        {
            UnequipItem(item);
        }

        // Check if the item is in the hotbar and clear the corresponding slot
        for (int i = 0; i < hotbarItems.Length; i++)
        {
            if (hotbarItems[i] == item)
            {
                hotbarItems[i] = null;
            }
        }

        // Update the displayed inventory
        ListItems();
    }
    public bool EquipItemToHotbarSlot(Items item, int slot)
    {
        return hotbarItems[slot] = item;
    }

    public void SetColliders()
    {

    }

}

