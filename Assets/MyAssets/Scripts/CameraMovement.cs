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

    public RectTransform inventoryPanel; // Reference to your inventory panel

    // Store the initial position and rotation of the player and camera
    private Vector3 currPlayerPosition;
    private Quaternion currPlayerRotation;
    private Vector3 currCameraPosition;
    private Quaternion currCameraRotation;
    private bool isInInventoryMode = false;


    void Start()
    {
        cameraTransform = transform; // Cache the camera's transform
        // Set the initial camera distance
        UpdateCameraDistance(cameraDistance);
        // Store the initial position and rotation of the player and camera
        currPlayerPosition = player.position;
        currPlayerRotation = player.rotation;
        currCameraPosition = cameraTransform.position;
        currCameraRotation = cameraTransform.rotation;
    }
    
    void LateUpdate()
    {
        
        // Capture scroll wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        //Capture player and camera current pos and rot
        currPlayerPosition = player.position;
        currPlayerRotation = player.rotation;
        currCameraPosition = cameraTransform.position;
        currCameraRotation = cameraTransform.rotation;
        // Adjust camera distance based on scroll input
        cameraDistance = Mathf.Clamp(cameraDistance - scrollInput * zoomSpeed, minDistance, maxDistance);
        UpdateCameraDistance(cameraDistance);

        // Check for right-click input to enable or disable camera rotation
        if (IsMouseOverInventory())
        {
            // Handle inventory mode logic here
            // ...
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isRotating = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isRotating = false;
            }
        }
        
        if (isRotating)
        {
            RotatePlayerWithMouse();
        }
        else
        {
            CameraFollowPlayer();
        }

        float rotateInput = Input.GetAxis("Horizontal");
        if (rotateInput != 0)
        {
            // Update mouseX based on player's rotation
            mouseX = player.rotation.eulerAngles.y;
        }
    }

    void UpdateCameraDistance(float distance)
    {
        // Update the camera's position based on the provided distance
        offset = Quaternion.Euler(mouseY, mouseX, 0) * Vector3.forward * -distance;
        targetPosition = player.position + offset;
        cameraTransform.position = targetPosition;
    }

    void RotatePlayerWithMouse()
    {

        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -45, 45); // Limit vertical camera rotation
        offset = Quaternion.Euler(mouseY, 0, 0) * Vector3.forward * -cameraDistance;
        targetPosition = player.position + offset;
        player.position = currPlayerPosition;
        player.rotation = currPlayerRotation;
        // Rotate both the camera and the player
        player.rotation = Quaternion.Euler(0, mouseX, 0);
        cameraTransform.LookAt(player);
    }
    
    void CameraFollowPlayer()
    {
        offset = Quaternion.Euler(mouseY, 0, 0) * Vector3.forward * -cameraDistance;
        targetPosition = player.TransformPoint(offset);
        cameraTransform.position = targetPosition;
        
        // Make the camera look at the player
        cameraTransform.LookAt(player);
    }

    bool IsMouseOverInventory()
    {
        if (inventoryPanel == null)
        {
            // No inventory panel specified, return false
            return false;
        }
        if(!inventoryPanel.gameObject.activeSelf)
        {
            return false;
        }

        // Convert mouse position to local coordinates of the inventory panel
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            inventoryPanel, Input.mousePosition, null, out Vector2 localPoint);

        // Check if the localPoint is within the inventory panel's boundaries
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, Input.mousePosition);
    }
}
