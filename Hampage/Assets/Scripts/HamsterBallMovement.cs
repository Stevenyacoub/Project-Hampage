using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

/* HamsterBallMovement uses the main camera and hamster ball rigidbody to determine how the ball
 * should move once the player is inside
 */
public class HamsterBallMovement : MonoBehaviour
{
    // References to the rigidbody and camera
    public Rigidbody rb;
    public Transform cam;

    // Player inputs for vertical and horizontal movement
    private float verticalInput;
    private float horizontalInput;

    // The speed at which the ball moves
    public float moveSpeed = 4f;

    // the vector for the movement direction once the orientation of the camera has been applied
    private Vector3 direction;

    //On Awake we want to instantiate our references
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    // Update() occurs every frame which makes it ideal for checking player inputs but not for the movement itself
    void Update()
    {
        PlayerInputs();
    }
    // FixedUpdate() occurs at a more consistent rate which makes it better for movement
    void FixedUpdate()
    {
        onMove();
    }

    // PlayerInputs() gets the input from the player
    void PlayerInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    /* OnMove takes the input as well as the camera orientation to determine the direction of the movement
    * Then we use the rigidbody AddForce method to apply movement force in the direction we calculated
    */
    public virtual void onMove()
    {
        // Cam forward and cam right bring the orientation of the player to the players inputs
        direction = cam.forward * verticalInput + cam.right * horizontalInput;

        // use rigidbodies AddForce method where the VEctor3 is our moveDirection above multiplied by the speed we want to move at.
        // The Forcetype set to Force (Continous force based on mass of object)
        rb.AddForce(direction.normalized * moveSpeed, ForceMode.Force);
    }
}
