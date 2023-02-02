using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public GameObject player { get; set; }
    public ControllerCharacter charController;
    public bool registered { get; set; }
    
    public PlayerManager playerManager;
    BlockMovement blockMovement;

    public bool performAction()
    {
        Debug.Log("Button pressed!");
        blockMovement = FindObjectOfType<BlockMovement>();
        blockMovement.OperateDoor();
        return true;
    }
    
    // Unity Methods

    //Awake gets called before the scene is finished loading (even before Start()!)
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
    }

    //When a rigidbody collides with the objects collider, this will be called
    // NOTE: Collider must have isTrigger set to true
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !registered){
            playerManager.registerInteractable(this);
        }
    }
    //Called when rigidbody leaves the collider
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player" && registered){
            playerManager.unregisterInteractable(this);
        }
    }

}
