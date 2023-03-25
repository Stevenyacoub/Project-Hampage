using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// LanceHolder is a type of interactable that allows the player to set their lance into it, transitioning the player from
// lance on to lance off. If the lance is in the holder then the player can choose to pick it up and then transition from lance 
// off to lance on

public class LanceHolder : Interactable
{
    public GameObject player;
    public InteractBox interactBox;
    public PlayerStateManager stateManager;

    public override bool performAction()
    {
        Debug.Log("performAction is doing something");
        LanceStateSwitch();
        return true;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactBox = player.transform.Find("InteractBox").GetComponent<InteractBox>();
        stateManager = player.GetComponent<PlayerStateManager>();
    }

    public void LanceStateSwitch(){
        interactBox.UnregisterInteractable(this);
        stateManager.SwitchState();
    }
}
