using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 moveDirection;
    private Animator anim;
    public Vector3 velocity;
    private float gravity = -9.8f;
    public float moveSpeed; // Adjust the movement speed.
    public float walkSpeed;
    public float runSpeed;
    public float rotationSpeed = 3.0f; // Adjust the rotation speed.
    public float jumpForce = 60.0f;
    public float groundRaycastDistance = 0.25f;
    public bool isGrounded;

    public LayerMask groundLayer;

    public bool isStrafing = true;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the game window.
    }

    void Update()
    {
        Move();
        
    }

    private void Move()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundRaycastDistance, groundLayer);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0,0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if(isGrounded)
        {
            if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if(moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
    void Idle()
    {
        anim.SetFloat("Speed", .5f, 0.1f, Time.deltaTime);
    }
    void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);

    }
    void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1.5f, 0.1f, Time.deltaTime);
    }
    void Jump()
    {
        anim.SetTrigger("Jump");
        velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
    }
}

/*
// Player Rotation
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // If Blocking, player strafes
        if(Input.GetMouseButton(1) && horizontalInput > 0.01)
        {
            isStrafing = true;
        }
        else
        {
            isStrafing = false;
            // Apply rotation only if Shift key is not held down
            Vector3 rotation = new Vector3(0, horizontalInput * rotationSpeed, 0);
            playerTransform.Rotate(rotation);
        }
        

        // Sprint/Walk logic

        // Player Horizontal Movement
        float horizontalSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        float horizontalInputValue = Input.GetAxis("Horizontal");
        moveDirection = playerTransform.right * horizontalInputValue * horizontalSpeed * Time.deltaTime;
        playerTransform.Translate(moveDirection, Space.World);

        // Player Vertical Movement
        float verticalSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        float verticalInputValue = Input.GetAxis("Vertical");
        moveDirection = playerTransform.forward * verticalInputValue * verticalSpeed * Time.deltaTime;
        playerTransform.Translate(moveDirection, Space.World);

        // Set speed to 0 if no input
        if (Mathf.Approximately(horizontalInputValue, 0f) && Mathf.Approximately(verticalInputValue, 0f))
        {
            anim.SetFloat("Speed", 0f);
        }
        else
        {
            // Set animation speed based on movement
            anim.SetFloat("Speed", Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1f);
        }
        
        
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
        */




