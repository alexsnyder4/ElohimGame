using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class DropdownSelectionScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetDropdownValue()
    {
        int index = dropdown.value;
        string selectedOption = dropdown.options[index].text;

        dropdown.RefreshShownValue(); // this is the key
        Debug.Log("Index is " + selectedOption);
        return selectedOption;
    }

    private void SetActionOfDropdown()
    {
        dropdown.onValueChanged.RemoveListener(ActionToCall);
        dropdown.onValueChanged.AddListener(ActionToCall);
    }

    public void ActionToCall(int selectedIndex)
    {
        Debug.Log("Selected Index " + selectedIndex);
    }
}
