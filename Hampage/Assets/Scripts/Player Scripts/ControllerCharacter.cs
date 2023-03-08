using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerCharacter : MonoBehaviour
{
    private Animator anim;
    public GameObject player;
    public CharacterController controller;
    public PlayerManager playerManager;
    public InteractBox interactBox;
    public Transform cam;
    public Rigidbody rb;

    public float moveSpeed = 10f;
    public float jumpForce = 2f;
    public float gravityScale = -20f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //These set and prevent the amount of knockback taken
    [SerializeField]
    public float knockbackForce = 10f;
    public float knockbackTime = 1f;
    private float knockbackCounter;
    public Vector3 knockVector;

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
    bool attackTriggered;

    void Awake()
    {
        input = new PlayerInput();
        player = GameObject.FindWithTag("Player");
        controller = GetComponent<CharacterController>();
        playerManager = GetComponent<PlayerManager>();
        anim = GetComponent<Animator>();

        // Adding our methods (On Move,etc) to the delegate for the motion (hence +=)
        // This makes our methods call-backs, and calls these methods when a condition is met
        // - There are 3 states an input can be in: Started (Button Down), performed (button held), and cancelled (released)
        // - Progress is only applicable to movement as multiple buttons can be held at once. Run and jump are binary so we only need start and stopped.
        input.CharacterControls.Move.started += onMoveInput;
        input.CharacterControls.Move.performed += onMoveInput;
        input.CharacterControls.Move.canceled += onMoveInput;

        input.CharacterControls.Run.started += onRunInput;
        input.CharacterControls.Run.canceled += onRunInput;

        input.CharacterControls.Jump.started += onJumpInput;
        input.CharacterControls.Jump.canceled += onJumpInput;

        //Basic Attack
        input.CharacterControls.Attack.started += onAttack;
        input.CharacterControls.Attack.canceled += onAttack;

        // We only care about button-down for interact, so just started state
        input.CharacterControls.Interact.started += onInteract;

    }

    float startKnocktime;

    void Update()
    {
        

        if(knockbackCounter <= 0)
        {
            if(!controller.enabled)
                controller.enabled = true;
                rb.isKinematic = true;
            
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
        }
        else
        {
            //Debug.Log("Force Adding: " + knockVector);
            
            rb.isKinematic = false;
            rb.AddForce(knockVector);
            knockbackCounter -= Time.deltaTime;
        }

        // This is the typical update used, without knockback functionality
        // TO-DO: Delete this call & the method associated once knockback works

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
        if(input != null)
            input.CharacterControls.Enable();
    }
    //Disables if averse occurs
    void OnDisable() {
        Debug.Log("Disabled Controls");
        if(input != null)
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
    // Callbacks to be executed on input update
    void onRunInput(InputAction.CallbackContext context){
        runTriggered = context.ReadValueAsButton();
    }
    
    void onJumpInput(InputAction.CallbackContext context){
        jumpTriggered = context.ReadValueAsButton();
    }

    void onInteract(InputAction.CallbackContext context){
        interactBox.interact();
    }

    void onAttack(InputAction.CallbackContext context)
    {
        attackTriggered = context.ReadValueAsButton();
        anim.SetTrigger("WhackInput");
    }

    //Method called by other scripts to fling player back
    //Sets timer in the Update method in this script
    public void Knockback(Vector3 direction)
    {
        knockbackCounter = knockbackTime;
        knockVector = direction * knockbackForce;
        controller.enabled = false;
    }
}
