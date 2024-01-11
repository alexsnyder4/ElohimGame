using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public bool isInCombat = false;
    public GameObject weaponInHand;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        isInCombat = CheckIfInCombat();

        UpdateLayerWeights();

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("Blocking", true);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("Blocking", false);
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
        return true;
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
        weaponInHand = GetComponent<PlayerInteraction>().rightHand.GetChild(0).gameObject;
    }
}
