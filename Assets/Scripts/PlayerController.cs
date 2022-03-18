using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove { get; private set; } = true;
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    private bool shouldJump => Input.GetKey(jumpKey) && characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool canJump = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Player Movement")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float gravity = 30.0f;
    [SerializeField] private float jumpForce = 8.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    private Camera playerCamera;
    
    

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    [Header("Raycast Parameters")]
    [SerializeField] private float interactionDistance = default;
    [SerializeField] public CharacterController characterController;
    public Transform teleportTarget;



    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponentInChildren<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
       
    }

    void Update()
    {
        if (canMove)
        {
            HandleMovementInput();
            HandleMouseMovement();

            if (canJump)
                HandleJump();

            ApplyFinalMovements();
        }
        if(canInteract)
        {
            CastRay();
        }
    }


    private void HandleMovementInput()
    {
        currentInput = new Vector2((isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseMovement()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        Quaternion quaternion = Quaternion.Euler(x: rotationX, 0, 0);
        playerCamera.transform.localRotation = quaternion;
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (shouldJump)
            moveDirection.y = jumpForce;
    }

    void CastRay()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, interactionDistance);
        if (hit)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                if (hitObject.gameObject.layer == 7)
                {
                    if(hitObject.CompareTag("Teleport"))
                    {
                        teleportTarget = hitInfo.transform.GetChild(0); // Gets Child of Parent
                        characterController.transform.position = teleportTarget.transform.position;
                    }
                    else
                    {
                        hitObject.GetComponent<InteractableObject>().Interact();
                    }
                }
            }
        }
    }
}
