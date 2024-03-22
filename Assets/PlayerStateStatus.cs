using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStateStatus : MonoBehaviour
{
    [SerializeField] private GPlayer player;
    [SerializeField] private TMP_Text stateTextComponent; // Reference to the TextMeshPro component
    [SerializeField] private PlayerMovementStateMachine playerMovementStateMachine; // Reference to the player's movement state machine
    [SerializeField] private bool isInCombat; // Boolean flag to indicate if the player is in combat

    private void Update()
    {
        
        // Get the name of the current player state
        string currentState = player.GetCurrentStateName();

        // Construct the text to display
        string displayText = currentState + "\n";
        displayText += "Combat: " + isInCombat;

        // Update the TextMeshPro component with the display text
        if (stateTextComponent != null)
        {
            stateTextComponent.text = displayText;
        }
    }
}