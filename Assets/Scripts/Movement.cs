using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
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
        Vector3 rotation = new Vector3(0, horizontalInput * rotationSpeed, 0);
        playerTransform.Rotate(rotation);

        // Player Vertical Movement
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = playerTransform.forward * verticalInput * moveSpeed * Time.deltaTime;
        playerTransform.Translate(moveDirection, Space.World);
        if (verticalInput > 0) //forward
        {
            isRunning = true;
            isWalkingBack = false;
            runJump = true;
            anim.SetBool("isRunning", isRunning);
            anim.SetBool("runJump", runJump);
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                jump();
            }
        }
        else if (verticalInput < 0)
        {
            isRunning = false;
            isWalkingBack = true;
            anim.SetBool("isWalkingBack", isWalkingBack);

        }
        else
        {
            isRunning = false;
            isWalkingBack = false;
            anim.SetBool("isRunning", isRunning);
        }

        if (IsGrounded())
        {
            isJumping = false;
            anim.SetBool("Jump", isJumping);
            anim.SetBool("runJump", isJumping);
        }

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() && verticalInput == 0)
        {
            isJumping = true;
            anim.SetBool("Jump", isJumping);
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






