using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public PlayerData playerData;
    public Transform playerTransform;
    private Vector3 moveDirection;
    private Animator anim;
    public float moveSpeed = 5.0f; // Adjust the movement speed.
    public float sprintSpeed = 10.0f; //Adjust movement speed
    public float rotationSpeed = 3.0f; // Adjust the rotation speed.
    public float jumpForce = 60.0f;
    public float groundRaycastDistance = 0.25f;

    public LayerMask groundLayer;
    private Rigidbody rb;

    private bool isRunning = false;
    private bool isJumping = false;
    private bool isWalkingBack = false;
    private bool runJump = false;
    private bool backJump = false;
    public bool isStrafing = true;
    private bool isStrafingR = false;
    private bool isStrafingL = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the game window.
    }

    void Update()
    {
    
    // Player Rotation
    float horizontalInput = Input.GetAxis("Horizontal");

    if (!Input.GetMouseButtonDown(1))
    {
        isStrafing = false;
        // Apply rotation only if Shift key is not held down
        Vector3 rotation = new Vector3(0, horizontalInput * rotationSpeed, 0);
        playerTransform.Rotate(rotation);
    }
    if(Input.GetMouseButton(1))
    {
        isStrafing = true;
    }
        //Player Horizontal Movement
        //Sprint/Walk logic
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            //walk
            moveDirection = playerTransform.right * horizontalInput * moveSpeed * Time.deltaTime;
            playerTransform.Translate(moveDirection, Space.World);
        }
        
        else
        { 
            //Sprint
            
            moveDirection = playerTransform.right * horizontalInput * sprintSpeed * Time.deltaTime;
            playerTransform.Translate(moveDirection, Space.World);
        }

        float verticalInput = Input.GetAxis("Vertical");

        // Player Vertical Movement
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection = playerTransform.forward * verticalInput * moveSpeed * Time.deltaTime;
            playerTransform.Translate(moveDirection, Space.World);
        }

        else
        {
   
            //sprint
            
            moveDirection = playerTransform.forward * verticalInput * sprintSpeed * Time.deltaTime;
            playerTransform.Translate(moveDirection, Space.World);
        }
    
    
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






