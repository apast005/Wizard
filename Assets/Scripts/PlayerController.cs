using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove { get; private set; } = true;
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canInteract = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;

    [Header("Player Movement")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    [Header("Raycast Parameters")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = 7;
    private InteractableObject currentInteractable;


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
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
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

    void CastRay()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, interactionDistance);
        if (hit)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                if(hitObject.gameObject.layer == 7)
                {
                    hitObject.GetComponent<InteractableObject>().Interact();
                }
            }
        }
    }

        
}
