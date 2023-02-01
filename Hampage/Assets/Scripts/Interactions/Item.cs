using UnityEngine;

public class Item : Interactable
{
    // Items are things that can be obtained by the player

    PlayerManager playerManager;

    void Start() {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public override bool performAction()
    {
        playerManager.addToInventory(this);
        playerManager.unregisterInteractable(this);
        Destroy(this.gameObject);
        return true;
        
    }

}
