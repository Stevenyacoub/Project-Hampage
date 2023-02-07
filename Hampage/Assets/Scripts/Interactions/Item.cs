using UnityEngine;

public class Item : Interactable
{
    // Items are things that can be obtained by the player

    PlayerManager playerManager;
    InteractBox interactBox;

    void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        interactBox = player.transform.Find("InteractBox").GetComponent<InteractBox>();
    }

    public override bool performAction()
    {
        playerManager.addToInventory(this);
        interactBox.UnregisterInteractable(this);
        Destroy(this.gameObject);
        return true;
        
    }

}
