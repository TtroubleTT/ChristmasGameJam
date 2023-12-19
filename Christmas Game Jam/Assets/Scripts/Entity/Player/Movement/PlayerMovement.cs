using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private AnimationManager animationManager;

    [Header("Speed")]
    [SerializeField] private float walkSpeed = 12f;
    [SerializeField] private float sprintSpeed = 20f;
    [SerializeField] private float crouchSpeed = 5f;
    private float _currentSpeed;
    
    [Header("Key binds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] public LayerMask groundMask;
    private bool _isGrounded;
    
    [Header("Physics")]
    [SerializeField] private float gravity = - 9.81f;
    [HideInInspector] public bool useGravity = true;
    [HideInInspector] public Vector3 velocity;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 3f;
    private bool _canDoubleJump = false;

    [Header("Crouching")]
    [SerializeField] private float crouchYScale;
    private float _startYScale;
    private bool _isCrouching = false;
    private double _fallTime = 0.0;
    private bool _jumped = false;

    // Movement States
    [HideInInspector] public MovementState movementState;

    public enum MovementState
    {
        Walking,
        Sprinting,
        Crouching,
        Air,
        Falling,
    }

    public bool IsGrounded()
    {
        return _isGrounded;
    }
    
    private void Start()
    {
        _startYScale = transform.localScale.y;
        animationManager = GetComponent<AnimationManager>();
    }

    private void Update()
    {
        // Handles what movement state we are in
        MovementStateHandler();
        
        // Resets falling velocity if they are no longer falling
        ResetVelocity();
        
        // Movement
        MoveInDirection();

        // Jumping
        CheckJump();
        
        // Crouching
        CheckCrouch();
        
        // Force standing if player isn't trying to crouch and is no longer under object
        ForceStandUp();

        // Gravity
        Gravity();
    }

    private void MovementStateHandler()
    {
        // Determines the movement state and speed based on different conditions
        if (_isCrouching)
        {
            movementState = MovementState.Crouching;
            _currentSpeed = crouchSpeed;
        }
        else if (_isGrounded && Input.GetKey(sprintKey))
        {
            movementState = MovementState.Sprinting;
            _currentSpeed = sprintSpeed;
        }
        else if (_isGrounded)
        {
            movementState = MovementState.Walking;
            _currentSpeed = walkSpeed;
        }
        else
        {
            if (_fallTime < 0.35 && !_jumped)
            {
                movementState = MovementState.Falling;
                _fallTime += Time.deltaTime;
            }
            else
                movementState = MovementState.Air;
        }
    }

    private void ResetVelocity()
    {
        // Sphere casts to check for ground
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Makes it so we arent changing velocity when on ground not falling
        if (_isGrounded && velocity.y < 0)
        {
            _jumped = false;
            _fallTime = 0.0;
            _canDoubleJump = true;
            velocity.y = -2f;
        }
    }

    private void MoveInDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Transform myTransform = transform;
        Vector3 move = myTransform.right * x + myTransform.forward * z; // This makes it so its moving locally so rotation is taken into consideration

        controller.Move(move * (_currentSpeed * Time.deltaTime)); // Moving in the direction of move at the speed
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            switch (_isGrounded || movementState == MovementState.Falling)
            {
                case true when movementState != MovementState.Crouching:
                    _jumped = true;
                    DoJump();
                    break;
                case false when movementState is MovementState.Air or MovementState.Falling && _canDoubleJump:
                    _canDoubleJump = false;
                    DoJump();
                    break;
            }
        }
    }

    private void DoJump() => velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    
    private bool IsUnderObject()
    {
        float heightAbove = controller.height - crouchYScale; // height length between full stand and crouch
        
        // Ray casts upwards an amount to check if you are under and object. If the raycast hits nothing then you are above ground.
        return Physics.Raycast(transform.position, Vector3.up, heightAbove, groundMask);
    }

    private void CheckCrouch()
    {
        Vector3 localScale = transform.localScale;
        
        // If we push down the crouch key and we are crouching (not wall running) we decrease model size
        if (Input.GetKeyDown(crouchKey))
        {
            animationManager.PlayPlayerAnimation(AnimationManager.AnimationType.Crouch);
            transform.localScale = new Vector3(localScale.x, crouchYScale, localScale.z);
            _isCrouching = true;
        }

        // When releasing crouch key sets our scale back to normal
        if (Input.GetKeyUp(crouchKey) && !IsUnderObject())
        {
            animationManager.PlayPlayerAnimation(AnimationManager.AnimationType.Idle);
            transform.localScale = new Vector3(localScale.x, _startYScale, localScale.z);
            _isCrouching = false;
        }
    }

    private void ForceStandUp()
    {
        if (_isCrouching && !Input.GetKey(crouchKey) && !IsUnderObject())
        {
            animationManager.PlayPlayerAnimation(AnimationManager.AnimationType.Idle);
            Vector3 localScale = transform.localScale;
            transform.localScale = new Vector3(localScale.x, _startYScale, localScale.z);
            _isCrouching = false;
        }
    }

    private void Gravity()
    {
        // If we are currently using gravity this makes us fall
        if (useGravity)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
