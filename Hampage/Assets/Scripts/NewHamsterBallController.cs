using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class NewHamsterBallController : MonoBehaviour
{
    protected Animator anim;
    [SerializeReference] public CharacterController controller;
    [SerializeReference] public Transform cam;

    [SerializeReference] public float moveSpeed = 2f;
    [SerializeReference] public float jumpForce = 5f;
    [SerializeReference] public float gravityScale = -20f;
    [SerializeReference] public float turnSmoothTime = 0.1f;
    [SerializeReference] public float turnSmoothVelocity;

    [SerializeReference] public Transform groundCheck;
    [SerializeReference] public float groundDistance = 0.4f;
    [SerializeReference] public LayerMask groundMask;

    [SerializeReference] public float knockbackForce = 1f;
    [SerializeReference] public float knockbackTime = 1f;
    [SerializeReference] protected float knockbackCounter;

    //private Vector3 moveDirection;
    protected Vector3 velocity;
    protected bool isGrounded;

    // -- / For new Input System:
    protected PlayerInput ballInput;
    protected Vector3 currMovement;
    public float runMultiplier = 3f;
    protected float targetAngle;
    protected bool runTriggered;
    protected bool jumpTriggered;
    protected bool attackTriggered;

    public virtual void Awake()
    {
        ballInput = new PlayerInput();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        // Adding our methods (On Move,etc) to the delegate for the motion (hence +=)
        // This makes our methods call-backs, and calls these methods when a condition is met
        // - There are 3 states an input can be in: Started (Button Down), performed (button held), and cancelled (released)
        // - Progress is only applicable to movement as multiple buttons can be held at once. Run and jump are binary so we only need start and stopped.
        ballInput.CharacterControls.Move.started += onMoveInput;
        ballInput.CharacterControls.Move.performed += onMoveInput;
        ballInput.CharacterControls.Move.canceled += onMoveInput;

        ballInput.CharacterControls.Run.started += onRunInput;
        ballInput.CharacterControls.Run.canceled += onRunInput;

        ballInput.CharacterControls.Jump.started += onJumpInput;
        ballInput.CharacterControls.Jump.canceled += onJumpInput;

    }

    public virtual void Update()
    {
        velocity.y += gravityScale * Time.deltaTime;
        // !! Not reccomended to use .Move() twice, but I haven't been able to figure out how to combine
        controller.Move(velocity * Time.deltaTime);
        handleRotation();
        //If not running or grounded, use normal movement, else use runMultiplier
        Vector3 moveDir = (!(runTriggered && isGrounded) ? currMovement : new Vector3(currMovement.x * runMultiplier, currMovement.y, currMovement.z * runMultiplier));
        controller.Move(moveDir * moveSpeed * Time.deltaTime);
        handleGravity();

    }

    public virtual void handleRotation()
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public virtual void handleGravity()
    {
        
        /*Debug.Log(groundCheck);
        Debug.Log(groundDistance);
        Debug.Log(groundMask);*/
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Debug.Log(isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (jumpTriggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravityScale);
        }
    }

    // -- // -- // USER INPUT

    //Enables our input while our object is alive
    public virtual void OnEnable()
    {
        if (ballInput != null)
        {
            ballInput.CharacterControls.Enable();
        }
    }
    //Disables if averse occurs
    //Disables if averse occurs
    public virtual void OnDisable()
    {
        if (ballInput != null)
            ballInput.CharacterControls.Disable();
    }
    // Callback to be executed on movement update
    public virtual void onMoveInput(InputAction.CallbackContext context)
    {
        //Mapping our 2 dimensional movement onto 3 dimensional space (x->x, y->z)
        //InputActions PlayerInput auto-normalizes for us 
        currMovement.x = context.ReadValue<Vector2>().x;
        currMovement.z = context.ReadValue<Vector2>().y;

        //Only if our movement is above a threshold, take camera into consideration when chosing move direction
        if (currMovement.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(currMovement.x, currMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            currMovement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
    }
    public virtual void onRunInput(InputAction.CallbackContext context)
    {
        runTriggered = context.ReadValueAsButton();
    }

    public virtual void onJumpInput(InputAction.CallbackContext context)
    {
        jumpTriggered = context.ReadValueAsButton();
    }


}
