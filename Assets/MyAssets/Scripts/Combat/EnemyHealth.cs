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
    private void Update()
    {
        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if(target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }
    */
    public void TakeDamage(int damage)
    {
        Debug.Log("took Damage");
        // Reduce the current health by the damage amount
        currentHealth -= damage;
        ui.UpdateHealthBar(currentHealth, maxHealth);
        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform death-related logic (e.g., play death animation, remove the enemy from the scene)
        Destroy(gameObject);
    }
}

