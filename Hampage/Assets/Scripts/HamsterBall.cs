using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* HamsterBall is an interactable game object that the player can "ride". While in the ball the player
 * cannot use attacks, interact with objects, or jump. The player can leave the ball at anytime by pressing
 * the e button, the same as when they first interacted with it.
 */
public class HamsterBall : Interactable
{
    public GameObject player;
    //public PlayerStateManager stateManager;
    public CharacterController playerController;
    public HamsterBallMovement ballScript;
    public Rigidbody rb;
    //public Transform mapI;

    [SerializeField]
    GameObject mapIcon;

    // Vectors for putting the  player into the ball
    Vector3 insideBall;
    Vector3 euler = new Vector3(0, -93, 0);
    
    // Overriden performAction() from Interactable class
    public override bool performAction()
    {
        // We need to check wether the player is in the ball or not in order to take the correct action.
        if(ballScript.enabled ==false)
        {
            EnableMovement();
        }
        else { 
            DisableMovement();
        }
        return true;
    }

    void Awake()
    {
        
        // Instantiate the player objects
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<CharacterController>();
        //stateManager = player.GetComponent<PlayerStateManager>();

        //For the MapIcon of the Hamster Ball we do not want it to rotate around the ball so we need to set it to a rotation
        /*mapI = this.GameObject.transform.GetChild(1);
        mapI.transform.eulerAngles.y = 180;*/

        // Instantiate the hamster ball movement script
        ballScript = GetComponent<HamsterBallMovement>();
        rb = GetComponent<Rigidbody>();
        // Set to false because we dont want it to move until interacted with
        ballScript.enabled = false;
        rb.isKinematic = true;
    }

    /* EnableMovement() is invoked when the player interacts with the ball from the outside.
     * It disables the playermovement, enables the hamster ball movement, and then puts the 
     * player model into the ball.
     */
    public void EnableMovement()
    {
        // Makes the player object a child of the hamsterball object
        player.transform.SetParent(transform);
        // Disables player collider then moves him into the ball with correct orientation
        player.GetComponent<SphereCollider>().enabled = false;
        insideBall = transform.position;
        player.transform.position = insideBall;
        player.transform.Rotate(euler, Space.World);

        // Enable the ball movement and disable player movement
        ballScript.enabled = true;
        rb.isKinematic = false;
        playerController.enabled = false;
    }

    /* DisableMovement() is invoked when the player interacts with the ball from the inside.
     * It enables the playermovement, disables the hamster ball movement, and then puts the 
     * player model outside the ball.
     */
    public void DisableMovement() {
        // Removes the player as a child of the Hamster Ball
        Transform childToRemove = transform.Find("Player");
        childToRemove.parent = null;

        // Enable the players collider, and put him outside the ball
        player.GetComponent<SphereCollider>().enabled = true;
        insideBall = transform.position;
        player.transform.position = insideBall;

        // Disable ball controller and enable player controller
        ballScript.enabled = false;
        rb.isKinematic = true;
        playerController.enabled = true;

        // Reset mapIcon to not have rolled with ball
        mapIcon.transform.rotation = Quaternion.Euler(90,0,0);
    }
   

}
