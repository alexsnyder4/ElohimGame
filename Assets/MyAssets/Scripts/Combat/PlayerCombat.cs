using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public bool isInCombat = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isInCombat = CheckIfInCombat();
        UpdateLayerWeights();
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))//Left Click
        {
            animator.SetTrigger("Attack");
        }
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

}
