using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{ 

    public float maxHealth;
    public float currentHealth;
    public Items itemsComponent;
    public ItemController ic;
    public EnemyUI ui;
    public float moveSpeed = 5f;
    Rigidbody rb;
    Transform target;
    Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        ic = GetComponent<ItemController>();
        itemsComponent = ic.items;

        if (itemsComponent == null)
        {
            Debug.LogError("Items component not found on the parent GameObject.");
        }
        maxHealth = itemsComponent.health;
        currentHealth = itemsComponent.health;
        ui.UpdateHealthBar(currentHealth,maxHealth);
    }
    /*
    
    */
    

}

