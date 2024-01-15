using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public List<GameObject> enemiesHit;
    private Items weapon;
    public int damageAmount;
    private bool canDealDamage;

    private void Start()
    {
        canDealDamage = false;
        weapon = GetComponent<ItemController>().items;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(canDealDamage)
        {
            // Check if the object collided with is an enemy
            if (other.CompareTag("Enemy"))
            {
                // Check if target was not hit yet
                if (!enemiesHit.Contains(other.gameObject))
                {
                    enemiesHit.Add(other.gameObject);
                    // Deal damage to the enemy
                    DealDamage(other.gameObject);
                }
            }
        }
    }

    private void DealDamage(GameObject enemy)
    {
        // Get the enemy's health component (you need to have a health component on your enemy)
        EnemyMovement enemyHealth = enemy.GetComponent<EnemyMovement>();

        if (enemyHealth != null)
        {
            // Deal damage to the enemy
            enemyHealth.TakeDamage(weapon.damage);
            Debug.Log(weapon.name + " : " + weapon.damage);
        }
    }

    public void StartAttack()
    {
        canDealDamage = true;
        enemiesHit.Clear();
    }
    public void EndAttack() // redundant currently not necessary
    {
        canDealDamage = false;
    }
}
