using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // Player's Transform
    public float cameraDistance = 5.0f;
    public float rotationSpeed = 2.0f;
    public float zoomSpeed = 3.0f;
    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;

    private Transform cameraTransform;
    private float mouseX, mouseY;
    private bool isRotating;
    private Vector3 offset;
    private Vector3 targetPosition;

    void Start()
    {
        cameraTransform = transform; // Cache the camera's transform
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock the cursor
        Cursor.visible = false;

        // Set the initial camera distance
        UpdateCameraDistance(cameraDistance);
    }

    void LateUpdate()
    {
        // Capture scroll wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Adjust camera distance based on scroll input
        cameraDistance = Mathf.Clamp(cameraDistance - scrollInput * zoomSpeed, minDistance, maxDistance);
        UpdateCameraDistance(cameraDistance);

        // Check for right-click input to enable or disable camera rotation
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
            // When mouse is released, revert to the starting position
        }

        if (isRotating)
        {
            MoveCameraWithMouse();
        }
        else
        {
            CameraFollowPlayer();
        }
    }

    void UpdateCameraDistance(float distance)
    {
        // Update the camera's position based on the provided distance
        Vector3 offset = Quaternion.Euler(mouseY, mouseX, 0) * Vector3.forward * -distance;
        Vector3 targetPosition = player.position + offset;
        cameraTransform.position = targetPosition;
    }

    void MoveCameraWithMouse()
    {
        
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -45, 45); // Limit vertical camera rotation
        offset = Quaternion.Euler(mouseY, mouseX, 0) * Vector3.forward * -cameraDistance;
        targetPosition = player.position + offset;
        //Vector3 wantedPosition = targetFollow.position + rotation * dir;
        //transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * orbitDamp);
        //transform.LookAt (targetFollow.position);

        // Make the camera look at the player
        cameraTransform.LookAt(player);
    }

    void CameraFollowPlayer()
    {
        mouseX = 0;
        offset = Quaternion.Euler(mouseY, mouseX, 0) * Vector3.forward * -cameraDistance;
        targetPosition = player.TransformPoint(offset);
        cameraTransform.position = targetPosition;

        // Make the camera look at the player
        cameraTransform.LookAt(player);
    }
}
