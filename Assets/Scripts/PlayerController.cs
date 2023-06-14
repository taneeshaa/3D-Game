using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float walkingSpeed = 5.0f;
    [SerializeField] float flyingSpeed = 10f;
    [SerializeField] float jumpSpeed = .5f;
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] AudioSource Player_Jump;
    float lastGroundedTime;

    public bool IsGrounded => controller.isGrounded;

    CharacterController controller;
    internal Vector3 velocity;
    Vector2 look;

    bool wasGrounded;
    public string currentState;


    public event Action OnBeforeMove;
    public event Action<bool> onGroundStateChange;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction sprintAction;

    private Statee state;

    [SerializeField] private Animator animator;
    private enum Statee
    {
        Walking,
        Flying
    }
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        sprintAction = playerInput.actions["sprint"];
    }
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        switch (state)
        {
            case Statee.Walking:
                UpdateGravity();
                UpdateMovement();
                PlayerAnim();
                UpdateLook();
                
                break;

            case Statee.Flying:
                UpdateMovementFlying();
                UpdateLook();
                break;
        }


    }

    Vector3 getMovementInput(float speed, bool horizontal = true)
    {
        var moveInput = moveAction.ReadValue<Vector2>();
        var input = new Vector3();
        var referenceTransform = horizontal ? transform : cameraTransform;
        input += referenceTransform.forward * moveInput.y;
        input += referenceTransform.right * moveInput.x;
        input = Vector3.ClampMagnitude(input, 1f);
        var sprintInput = sprintAction.ReadValue<float>();
        var multiplier = sprintInput > 0 ? 1.5f : 1f;
        input *= speed * multiplier;
        return input;
    }

    

    void PlayerAnim()
    {

        float magnitude = controller.velocity.magnitude;
        animator.SetFloat("Blend", magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player_Jump.Play();
            animator.SetTrigger("jump");
        }
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }
    }
    void UpdateMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            animator.SetTrigger("jump");
            velocity.y += jumpSpeed;

        }
        var input = getMovementInput(walkingSpeed);

        var factor = acceleration * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateMovementFlying()
    {
        var input = getMovementInput(flyingSpeed, false);

        var factor = acceleration * Time.deltaTime;
        velocity = Vector3.Lerp(velocity, input, factor);

        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateLook()
    {
        var lookInput = lookAction.ReadValue<Vector2>();
        look.x += lookInput.x * mouseSensitivity;
        look.y += lookInput.y * mouseSensitivity;

        look.y = Mathf.Clamp(look.y, -89f, 89f);

        transform.localRotation = Quaternion.Euler(0, look.x, 0);
        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = IsGrounded ? -1f : velocity.y + gravity.y;
    }

    void OnToggleFlying()
    {
        state = state == Statee.Flying ? Statee.Walking : Statee.Flying;
    }
}