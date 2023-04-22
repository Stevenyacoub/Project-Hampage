using UnityEngine;

public class Item : Interactable
{
    
    // Created by Giovanni Quevedo
    // -- Items are things that can be obtained by the player
    // -- They are children of the Interactable class, and destroy themselves when interacted with

    PlayerManager playerManager;
    InteractBox interactBox;

    // Gain references to our player and it's classes on startup
    void Start() {
        GameObject player = GameManager.staticPlayer;
        playerManager = player.GetComponent<PlayerManager>();
        interactBox = player.transform.Find("InteractBox").GetComponent<InteractBox>();        
    }

    // Destroys itself and updates the player's inventory
    public override bool performAction()
    {
        playerManager.addToInventory(this);
        interactBox.UnregisterInteractable(this);
        Destroy(this.gameObject);
        return true;
        
    }

}
