using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public bool isInCombat = false;
    public GameObject weaponInHand;
    public PlayerData pd;
    public PlayerUI ui;
    public bool blocking;
    void Start()
    {
        animator = GetComponent<Animator>();
        blocking = false;
    }

    void Update()
    {
        
        CheckIfInCombat();

        UpdateLayerWeights();

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("Blocking");
            blocking = true;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetTrigger("Blocking");
            blocking = false;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))//Left Click
        {
            UpdateWeapon();
            animator.SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {
        
    }

    bool CheckIfInCombat()
    {
        if(Input.GetKey(KeyCode.L))
        {
            isInCombat = !isInCombat;
        }
        return isInCombat;
    }

    void UpdateLayerWeights()
    {
        // Get the current layer weights
        float combatLayerWeight = isInCombat ? 1.0f : 0.0f; // 1.0f when in combat, 0.0f otherwise

        // Set the layer weights based on the combat state
        animator.SetLayerWeight(0, 1 - combatLayerWeight); // Reduce influence of base layer when in combat
        animator.SetLayerWeight(1, combatLayerWeight); // Increase influence of combat layer when in combat
    }

    public void StartDealDamage()
    {
        weaponInHand.GetComponent<WeaponScript>().StartAttack();
    }
    public void EndDealDamage()
    {
        weaponInHand.GetComponent<WeaponScript>().EndAttack();
    }
    public void UpdateWeapon()
    {
        weaponInHand = GetComponentInParent<PlayerInteraction>().rightHand.GetChild(0).gameObject;
    }
    public void TakeDamage(float damage)
    {
        if(blocking == true)
        {
            damage *= .5f;
        }
        pd.health -= damage;

        ui.UpdateHealthBar(pd.health,pd.maxHealth);
        if (pd.health <= 0)
        {
            //animator.SetTrigger("Die");
            Debug.Log("Player is ded");

        }
    }
}
