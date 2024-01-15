using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public GameObject weaponInHand;
    public bool weaponStowed;
    public PlayerData pd;
    public PlayerUI ui;
    public bool blocking;
    private float autoStowTimer = 14f;


    void Start()
    {
        animator = GetComponent<Animator>();
        blocking = false;
        weaponStowed = true;
        StartCoroutine(AutoStowWeapon());
    }

    void Update()
    {
        UpdateWeapon();
        // Reset the timer when a click occurs
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            autoStowTimer = 7f;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (weaponStowed)
            {
                weaponStowed = false;
                CombatStance();
                animator.SetTrigger("drawWeapon");
            }
            animator.SetTrigger("Blocking");
            blocking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetTrigger("Blocking");
            blocking = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Left Click
        {
            Debug.Log("Click");
            
            if (weaponStowed)
            {
                Debug.Log("draw weapon loop");
                weaponStowed = false;
                CombatStance();
                animator.SetTrigger("drawWeapon");
            }
            animator.SetTrigger("Attack");
        }
        Debug.Log("Combat Layer Weight: " + animator.GetLayerWeight(1));
    }

    void FixedUpdate()
    {
        
    }

    public void CombatStance()
    {
        Debug.Log("Updating Weights");
        // Get the current layer weights
        float combatLayerWeight = 1.0f; // 1.0f when in combat, 0.0f otherwise
        Debug.Log("combat Layer weight: " + combatLayerWeight);
        // Set the layer weights based on the combat state
        animator.SetLayerWeight(0, 1 - combatLayerWeight); // Reduce influence of base layer when in combat
        animator.SetLayerWeight(1, combatLayerWeight); // Increase influence of combat layer when in combat
    }
    public void PassiveStance()
    {
        Debug.Log("Updating Weights");
        // Get the current layer weights
        float passiveLayerWeight = 0.0f; // 1.0f when in combat, 0.0f otherwise
        Debug.Log("combat Layer weight: " + passiveLayerWeight);
        // Set the layer weights based on the combat state
        animator.SetLayerWeight(0, passiveLayerWeight); // Reduce influence of base layer when in combat
        animator.SetLayerWeight(1, 1 - passiveLayerWeight); // Increase influence of combat layer when in combat
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
        if(GetComponentInParent<PlayerInteraction>().rightHand.GetChild(0).gameObject == null)
        {
            return;
        }
        else
        {
            weaponInHand = GetComponentInParent<PlayerInteraction>().rightHand.GetChild(0).gameObject;
        }
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
    IEnumerator AutoStowWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            autoStowTimer -= 1f;
            Debug.Log("tick");

            // Auto-stow the weapon after the specified delay
            if (autoStowTimer <= 0f)
            {
                weaponStowed = true;
                PassiveStance();
                animator.SetTrigger("sheathWeapon");
                autoStowTimer = 7f; // Reset the timer
            }
        }
    }
}
