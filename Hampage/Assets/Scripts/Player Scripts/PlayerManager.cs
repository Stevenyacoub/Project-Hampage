using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Class used to manage player data not related to input or interaction
    //For input based management, see ControllerCharacter
    //For interaction controls, see InteractBox

    // A list of "collected" items
    // !! TODO: Items currently delete themselves, so the items themselves can't be accessed!!
    // !! We need to build a inventory version of items when they're collected, this is only a temporary demonstration!!!
    List<Item> inventory;


    // Awake gets called before the first frame update
    void Awake(){
        // Instantiating an empty inventory
        inventory = new List<Item>();
    }

    // Update gets called each frame update
    void Update(){
        // Do something
    }

    // Method to add items to inventory
    // !! This is only temporary, as items destroy themselves when collected
    // TODO: Refactor to use an inventory version of items
    public void addToInventory(Item item){
        inventory.Add(item);
        Debug.Log("Inventory Count:" + inventory.Count);
    }

}
