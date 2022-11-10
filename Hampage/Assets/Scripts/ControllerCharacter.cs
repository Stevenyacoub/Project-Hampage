using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerCharacter : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float moveSpeed = 10f;
    public float jumpForce = 2f;
    public float gravityScale = -20f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //private Vector3 moveDirection;
    Vector3 velocity;
    bool isGrounded;

    // -- / For new Input System:
    PlayerInput input;
    Vector3 currMovement;
    public float runMultiplier = 3f;
    float targetAngle;
    bool runTriggered;
    bool jumpTriggered;

    void Awake()
    {
        input = new PlayerInput();
        controller = GetComponent<CharacterController>();

        // The following are adding our methods (On Move,etc) to the delegate for the motion (hence +=)
        // This essentially makes our methods call-backs, and calls these methods when a condition is met
        // - There are 3 states an input can be in: Started (Button Down), performed (button held), and cancelled (released)
        // - Progress is only applicable to movement as multiple buttons can be held at once. Run and jump are binary so we only need start and stopped.
        input.CharacterControls.Move.started += onMoveInput;
        input.CharacterControls.Move.performed += onMoveInput;
        input.CharacterControls.Move.canceled += onMoveInput;

        input.CharacterControls.Run.started += onRunInput;
        input.CharacterControls.Run.canceled += onRunInput;

        input.CharacterControls.Jump.started += onJumpInput;
        input.CharacterControls.Jump.canceled += onJumpInput;
    }

    void Update()
    {
        velocity.y += gravityScale * Time.deltaTime;
        // !! Not reccomended to use .Move() twice, but I haven't been able to figure out how to combine
        controller.Move(velocity * Time.deltaTime);

        handleRotation();

        //If not running or grounded, use normal movement, else use runMultiplier
        Vector3 moveDir = (!(runTriggered && isGrounded) ? currMovement : new Vector3(currMovement.x * runMultiplier, currMovement.y, currMovement.z * runMultiplier));
        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        //Commenting out '&& isGrounded' gives us a pseudo-dash that's fun but not practical. Comment out above and uncomment below to try:
        // //If not running or grounded, use normal movement, else use runMultiplier
        // Vector3 moveDir = (!(runTriggered && isGrounded) ? currMovement : new Vector3(currMovement.x * runMultiplier, currMovement.y, currMovement.z * runMultiplier));
        // controller.Move(moveDir * moveSpeed * Time.deltaTime); 
        
        handleGravity();

    }

    void handleRotation(){
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    void handleGravity(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

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
    void OnEnable(){
        input.CharacterControls.Enable();
    }
    //Disables if averse occurs
    void OnDisable() {
        input.CharacterControls.Disable();
    }
    // Callback to be executed on movement update
    void onMoveInput(InputAction.CallbackContext context){
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
    // Callback to be executed on Run update
    void onRunInput(InputAction.CallbackContext context){
        runTriggered = context.ReadValueAsButton();
    }
    // Callback to be executed on Jump update
    void onJumpInput(InputAction.CallbackContext context){
        jumpTriggered = context.ReadValueAsButton();
    }
}
