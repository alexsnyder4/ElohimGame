using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Image hpBar;
    

    void Start()
    {
        hpBar = GetComponent<Image>();
    }

    private void Update()
    {
        // Get the direction from the text to the camera
        Vector3 directionToCamera = Camera.main.transform.position - transform.position;

        // Create a rotation that looks at the camera
        Quaternion rotation = Quaternion.LookRotation(directionToCamera, Vector3.up);

        // Apply the rotation to the text
        transform.rotation = rotation;
    }

    public void UpdateHealthBar(float currentHp, float maxHp)
    {
        hpBar.fillAmount = currentHp / maxHp;
    }
}
