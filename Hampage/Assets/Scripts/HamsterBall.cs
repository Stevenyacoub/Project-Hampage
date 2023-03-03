using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HamsterBall : Interactable
{
    
    public GameObject player;
    public PlayerStateManager stateManager;
    public NewHamsterBallController ballController;
    public CharacterController playerController;
    

    Vector3 insideBall;
    Vector3 euler = new Vector3(0, -93, 0);
    
    public override bool performAction()
    {
        if (ballController.enabled == false)
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
        player = GameObject.FindGameObjectWithTag("Player");
        stateManager = player.GetComponent<PlayerStateManager>();
        if (stateManager == null)
        {
            Debug.Log("stateManager is null");
        }
        ballController = GetComponent<NewHamsterBallController>();
        ballController.enabled = false;
        playerController = player.GetComponent<CharacterController>();
        
    }

    public void EnableMovement()
    {
        player.transform.SetParent(transform);
        player.GetComponent<SphereCollider>().enabled = false;
        insideBall = transform.position;
        player.transform.position = insideBall;
        player.transform.Rotate(euler, Space.World);

        ballController.enabled = true;
        playerController.enabled = false;
    }

    public void DisableMovement() {
        Transform childToRemove = transform.Find("Player");
        childToRemove.parent = null;

        player.GetComponent<SphereCollider>().enabled = true;
        insideBall = transform.position;
        player.transform.position = insideBall; 

        ballController.enabled = false;
        playerController.enabled = true;
    }
    
}
