using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerCharacter : MonoBehaviour
{
    [SerializeReference] public CharacterController controller;
    [SerializeReference] public PlayerManager playerManager;
    [SerializeReference] public InteractBox interactBox;
    [SerializeReference] public Transform cam;
    [SerializeReference] public PlayerStateManager stateManager;


    [SerializeReference] public float moveSpeed = 10f;
    [SerializeReference] private float initialMoveSpeed = 10f;
    [SerializeReference] public float speedBoostCD = 4f;
    [SerializeReference] public float jumpForce = 2f;
    [SerializeReference] private float initialJumpForce = 2f;
    [SerializeReference] public float jumpBoostCD = 4f;
    [SerializeReference] public float gravityScale = -20f;
    [SerializeReference] public float turnSmoothTime = 0.1f;
    [SerializeReference] public float turnSmoothVelocity;

    [SerializeReference] public Transform groundCheck;
    [SerializeReference] public float groundDistance = 0.4f;
    [SerializeReference] public LayerMask groundMask;

    //private Vector3 moveDirection;
    protected Vector3 velocity;
    protected bool isGrounded;

    // -- / For new Input System:
    protected PlayerInput input;
    protected Vector3 currMovement;
    public float runMultiplier = 3f;
    protected float targetAngle;
    protected bool runTriggered;
    protected bool jumpTriggered;
    protected bool attackTriggered;

    // For knockback
    Rigidbody rb;
    float knockbackForce = 2f;
    [SerializeReference] public float knockbackTime = 1f;
    [SerializeReference] protected float knockbackCounter;
    public Vector3 knockVector;
    bool knockBackFlag = false;

     // Anmations
    //[SerializeField]protected Animator anim;
    bool hasJumped = false;
    public Animator hamsterMovementAnimator;

    public virtual void Awake()
    {
        input = new PlayerInput();
        controller = GetComponent<CharacterController>();
        playerManager = GetComponent<PlayerManager>();
        //anim = GetComponent<Animator>();
        stateManager = GetComponent<PlayerStateManager>();
        rb = GetComponent<Rigidbody>();

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

        // Don't detect collisions on start (prevent camera jitter)
        //rb.detectCollisions = false;

    }

    public virtual void Update()
    {
    
        if(knockbackCounter <= 0)
        {

            if(knockBackFlag == true){
                //rb.detectCollisions = false;
                controller.enabled = true;
                rb.isKinematic = true;
                
                knockBackFlag = false;
                velocity.y = -2f; 
            }
            
            velocity.y += gravityScale * Time.deltaTime;
            // !! Not reccomended to use .Move() twice, but I haven't been able to figure out how to combine

            // only moving controller if it's active (inactive when using hamsterball)
            if(controller.enabled)
              controller.Move(velocity * Time.deltaTime);

            handleRotation();

            //If not running or grounded, use normal movement, else use runMultiplier
            Vector3 moveDir;
            if(!(runTriggered && isGrounded)){
                moveDir = currMovement;
                hamsterMovementAnimator.SetFloat("runMultiplier",1);
            }else{
                moveDir = new Vector3(currMovement.x * runMultiplier, currMovement.y, currMovement.z * runMultiplier);
                hamsterMovementAnimator.SetFloat("runMultiplier",1.5f);
            }

             // only moving controller if it's active (inactive when using hamsterball)
            if(controller.enabled)
                controller.Move(moveDir * moveSpeed * Time.deltaTime);

            //Commenting out '&& isGrounded' gives us a pseudo-dash that's fun but not practical. Comment out above and uncomment below to try:
            //If not running or grounded, use normal movement, else use runMultiplier
            //Vector3 moveDir = (!(runTriggered && isGrounded) ? currMovement : new Vector3(currMovement.x * runMultiplier, currMovement.y, currMovement.z * runMultiplier));
            //controller.Move(moveDir * moveSpeed * Time.deltaTime); 
        }
        else
        {

            rb.AddForce(knockVector);
            knockbackCounter -= Time.deltaTime;
        }

        // If we're moving significantly, animate walk
        if(currMovement.magnitude >= 0.1f){
            
            hamsterMovementAnimator.SetBool("isWalking", true);

        }else{
            hamsterMovementAnimator.SetBool("isWalking", false);
        }

        handleGravity();
    }

    public virtual void handleRotation(){
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    

    public virtual void handleGravity(){
       
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(!isGrounded){
            // If not grounded, try to fall
            if(!hamsterMovementAnimator.GetBool("isFalling")){
                hamsterMovementAnimator.SetBool("isFalling",true);
            }
        }else{
            // If falling, stop
            if(hamsterMovementAnimator.GetBool("isFalling")){
                hamsterMovementAnimator.SetBool("isFalling",false);
            }

            bool reachedLand = hamsterMovementAnimator.GetCurrentAnimatorStateInfo(0).IsName("Land");
            bool reachedRun = hamsterMovementAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run");
            bool reachedIdle = hamsterMovementAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle");

            if( reachedLand || reachedIdle || reachedRun){
                //We've finished our jump, so we can now unblock transitions to falling
                hamsterMovementAnimator.SetBool("midJump",false);
                // Let the code know that we've finished jumping as well
                hasJumped = false;
            }
        
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        if (jumpTriggered && isGrounded)
        {
            if(!hasJumped){
                hasJumped = true;
                // We can jump, so trigger jump animation
                hamsterMovementAnimator.SetTrigger("startJumping");
                // Block transitions to Falling for the time being
                hamsterMovementAnimator.SetBool("midJump",true);
                // Let our code know we already triggered it (to prevent multiple triggers)
                
            }
                
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravityScale);
        }

       
    }

    // -- // -- // USER INPUT

    //Enables our input while our object is alive
    public virtual void OnEnable(){
        if(input != null)
            input.CharacterControls.Enable();
    }
    //Disables if averse occurs
    public virtual void OnDisable() {
        if(input != null)
            input.CharacterControls.Disable();
    }
    // Callback to be executed on movement update
    public virtual void onMoveInput(InputAction.CallbackContext context){
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

        // Let animator know to start walking
        //hamsterMovementAnimator.SetTrigger("startWalking");
    }
    // Callbacks to be executed on input update
    public virtual void onRunInput(InputAction.CallbackContext context){
        runTriggered = context.ReadValueAsButton();
    }

    public virtual void onJumpInput(InputAction.CallbackContext context){
        jumpTriggered = context.ReadValueAsButton();
    }

    protected void onInteract(InputAction.CallbackContext context){
        interactBox.interact();
        // Let animator know to start interacting
        hamsterMovementAnimator.SetTrigger("startInteracting");
    }

    protected void onAttack(InputAction.CallbackContext context)
    {
        attackTriggered = context.ReadValueAsButton();
        hamsterMovementAnimator.SetTrigger("JabInput");
        Invoke(nameof(ResetJab),0.2f);
    }

    //Hotfix to reset jab after calling 
    void ResetJab(){
        hamsterMovementAnimator.ResetTrigger("JabInput");
    }

    public void Knockback(Vector3 direction){
        
        knockBackFlag = true;
        rb.isKinematic = false;

        // Set the knockback counter for time
        knockbackCounter = knockbackTime;

        // Get the vector for knockback movement
        knockVector = direction * knockbackForce;
        //currMovement = direction * knockbackForce;

       
        controller.enabled = false;
        //rb.detectCollisions = true;


    }



    //Alan
    //enters when player colliedes with object
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //checks to see the tag of the object collided with
        switch (hit.gameObject.tag)
        {
            //if tag is 'SpeedBoost" change the player speed
            case "SpeedBoost":
                moveSpeed = 25f;
                break;

            //if tag is 'JumpPad' change the player jump force
            case "JumpPad":
                isGrounded = true;
                jumpForce = 16f;
                break;

            //if tag is 'Ground' return moveSpeed and jumpForce to original values
            case "Ground":
                moveSpeed = initialMoveSpeed;
                jumpForce = initialJumpForce;
                break;
        }
    }

}
