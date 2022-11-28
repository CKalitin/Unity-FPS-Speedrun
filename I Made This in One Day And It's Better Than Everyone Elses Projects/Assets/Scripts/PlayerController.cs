using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    #region Variables

    [Header("General")]
    [SerializeField] private bool lockRot = false;
    [Tooltip("This doesn't allow input from the mouse on the first frame. Otherwise mouse would snap to center, rotating the player, which is a bug.")]
    [SerializeField] private bool lockOnFirstUpdate = true;
    private bool firstUpdatePassed = false;
    int updates = 0;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [Tooltip("This is to fix acceleration when in air.")]
    [SerializeField] private float maxMovementSpeed;
    [Space]
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    [Space]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMovementSpeedMultiplier;

    bool readyToJump;
    Vector2 movementInput;
    Vector3 moveDirection;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Jumping - Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundLayerMask;
    bool grounded;
    
    [Header("Rotation")]
    [SerializeField] private Vector2 sensitivity;
    [Space]
    [Tooltip("Objects in this array will be only rotated on the y axis. Left and Right")]
    [SerializeField] private Transform[] yAxisRotation;
    [Tooltip("Objects in this array will be only rotated on both axis")]
    [SerializeField] private Transform[] fullAxisRotation;
    Vector2 cameraRot;

    [Header("Other")]
    [SerializeField] private Rigidbody rb;

    #endregion

    #region Core

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb.freezeRotation = true;
        readyToJump = true;

        if (lockOnFirstUpdate) {
            lockRot = true;
        }
    }

    private void Update() {
        PlayerMovementInputs();
        SpeedControl();

        RotatePlayer();
        
        if (!firstUpdatePassed & lockOnFirstUpdate) {
            if (updates > 3) {
                firstUpdatePassed = true;
                
                lockRot = false;
            }
            updates++;
        }
    }

    private void FixedUpdate() {
        MovePlayer();
    }
    
    private void PlayerMovementInputs() {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f, groundLayerMask); // Ground Check

        // Handle drag
        if (grounded) rb.drag = groundDrag;
        else rb.drag = rb.drag = airDrag;

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded) {
            Jump();
        }
    }

    #endregion

    #region Movement

    private void MovePlayer() {
        moveDirection = yAxisRotation[0].forward * movementInput.y + yAxisRotation[0].right * movementInput.x; // Get Movement Direction
        
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMovementSpeedMultiplier, ForceMode.Force);
    }

    private void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > maxMovementSpeed) {
            Vector3 limitedVel = flatVel.normalized * maxMovementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    #endregion

    #region Jumping

    private void Jump() {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset Y velocity
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        readyToJump = false;
        Invoke(nameof(ResetJumpReady), jumpCooldown);
    }

    private void ResetJumpReady() {
        readyToJump = true;
    }


    #endregion

    #region Rotation
    
    private void RotatePlayer() {
        if (!lockRot) {
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity.x, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity.y);

            cameraRot.y += mouseDelta.x;
            cameraRot.x -= mouseDelta.y;
            cameraRot.x = Mathf.Clamp(cameraRot.x, -90f, 90f); // Clamp x when looking up or down

            for (int i = 0; i < yAxisRotation.Length; i++) {
                yAxisRotation[i].rotation = Quaternion.Euler(0f, cameraRot.y, 0f);
            }

            for (int i = 0; i < fullAxisRotation.Length; i++) {
                fullAxisRotation[i].rotation = Quaternion.Euler(cameraRot.x, cameraRot.y, 0f);
            }
        }
    }

    #endregion
}
