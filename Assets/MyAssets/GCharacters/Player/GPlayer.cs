using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class GPlayer : MonoBehaviour
{
    [field: Header("References")]
   [field: SerializeField] public PlayerSO Data { get; private set; }
    public Transform MainCameraTransform {  get; private set; }
    public Rigidbody rb {  get; private set; }

    public PlayerInput Input { get; private set; }

    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        movementStateMachine = new PlayerMovementStateMachine(this);
        Input = GetComponent<PlayerInput>();
        MainCameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
        movementStateMachine.ChangeState(movementStateMachine.IdlingState);
    }
    private void Update()
    {
        movementStateMachine.HandleInput();

        movementStateMachine.Update();
    }
    private void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();
    }
}
