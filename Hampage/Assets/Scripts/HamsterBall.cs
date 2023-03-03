using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HamsterBall : Interactable
{
    public GameObject player;
    //public InteractBox interactBox;
    public PlayerStateManager stateManager;
    public CharacterController ballController;
    public CharacterController playerController;
    
    Vector3 insideBall;
    

    public override bool performAction()
    {
        player.transform.SetParent(transform);
        insideBall = transform.position;
        player.transform.position = insideBall;
        EnableMovement();
            
        return true;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //interactBox = player.transform.Find("InteractBox").GetComponent<InteractBox>();
        stateManager = player.GetComponent<PlayerStateManager>();
        if (stateManager == null)
        {
            Debug.Log("stateManager is null");
        }
        ballController = GetComponent<CharacterController>();
        ballController.enabled = false;
        playerController = player.GetComponent<CharacterController>();
    }

    public void EnableMovement()
    { 
        ballController.enabled = true;
        playerController.enabled = false;
    }
    
}
