using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{ 

    public float maxHealth;
    public float currentHealth;
    public Items itemsComponent;
    public EnemyUI ui;

    private void Start()
    {

        if (itemsComponent == null)
        {
            Debug.LogError("Items component not found on the parent GameObject.");
        }
        maxHealth = itemsComponent.health;
        currentHealth = itemsComponent.health;
        ui.UpdateHealthBar(currentHealth,maxHealth);
    }

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

