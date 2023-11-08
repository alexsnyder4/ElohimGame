using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public PlayerData playerData;
    private Transform playerTransform;
    private Vector3 moveDirection;
    private Animator anim;
    public float moveSpeed = 5.0f; // Adjust the movement speed.
    public float rotationSpeed = 5.0f; // Adjust the rotation speed.
    public float jumpForce = 60.0f;
    public float groundRaycastDistance = 0.1f;

    public LayerMask groundLayer;
    private Rigidbody rb;

    private bool isRunning = false;
    private bool isJumping = false;
    private bool isWalkingBack = false;
    private bool runJump = false;
    private bool backJump = false;
    private bool isStrafing = true;
    private bool isStrafingR = false;
    private bool isStrafingL = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerTransform = transform;
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the game window.
    }

    void Update()
    {
    
    // Player Rotation
    float horizontalInput = Input.GetAxis("Horizontal");
    
    if (!Input.GetKey(KeyCode.LeftShift))
    {
        isStrafing = false;
        // Apply rotation only if Shift key is not held down
        Vector3 rotation = new Vector3(0, horizontalInput * rotationSpeed, 0);
        playerTransform.Rotate(rotation);
    }
    moveDirection = playerTransform.right * horizontalInput * moveSpeed * Time.deltaTime;
    playerTransform.Translate(moveDirection, Space.World);

    // Player Vertical Movement
    float verticalInput = Input.GetAxis("Vertical");
    moveDirection = playerTransform.forward * verticalInput * moveSpeed * Time.deltaTime;
    playerTransform.Translate(moveDirection, Space.World);
    // Reset all movement-related animation parameters
    isRunning = false;
    isWalkingBack = false;
    //sending current position to player data scriptable object
    playerData.currPosition = playerTransform.position;
    if (verticalInput > 0) // forward
    {
        isRunning = true;
        runJump = Input.GetKeyDown(KeyCode.Space) && IsGrounded();
    }
    else if (verticalInput < 0)
    {
        isWalkingBack = true;
        isJumping = Input.GetKeyDown(KeyCode.Space) && IsGrounded();
    }
        
    else
    {
        isJumping = Input.GetKeyDown(KeyCode.Space) && IsGrounded();
    }
    if(horizontalInput > 0 && isStrafing == true)
    {
        isStrafingL = true;
    }
    else if(horizontalInput < 0 && isStrafing == true)
    {
        isStrafingR = true;
    }
    else
    {
        isStrafingL = false;
        isStrafingR = false;
    }

    // Update the animator parameters
    anim.SetBool("isRunning", isRunning);
    anim.SetBool("isWalkingBack", isWalkingBack);
    anim.SetBool("runJump", runJump);
    anim.SetBool("Jump", isJumping);
    anim.SetBool("isStrafingL", isStrafingL);
    anim.SetBool("isStrafingR", isStrafingR);
    isStrafing = true;

    if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
    {
        jump();
    }
}

    void jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        // Perform a downward raycast from the player's position
        return Physics.Raycast(transform.position, Vector3.down, groundRaycastDistance, groundLayer);
    }
}






