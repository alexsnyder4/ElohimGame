using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with is an enemy
        if (other.CompareTag("Enemy"))
        {
            // Deal damage to the enemy
            DealDamage(other.gameObject);
        }
    }

    private void DealDamage(GameObject enemy)
    {
        // Get the enemy's health component (you need to have a health component on your enemy)
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            // Deal damage to the enemy
            enemyHealth.TakeDamage(damageAmount);
        }
    }
}
