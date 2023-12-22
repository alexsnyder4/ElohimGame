using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{

    private void Update()
    {
        // Get the direction from the text to the camera
        Vector3 directionToCamera = Camera.main.transform.position - transform.position;

        // Create a rotation that looks at the camera
        Quaternion rotation = Quaternion.LookRotation(directionToCamera, Vector3.up);

        // Apply the rotation to the text
        transform.rotation = rotation;
    }

}
