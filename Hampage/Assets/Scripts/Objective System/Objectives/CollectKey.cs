using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour, IObjective
{
    // Created by Giovanni Quevedo
    // Objective stipulating an item must be collected
    [field: SerializeField]
    public bool complete { get; set; }

    [SerializeField]
    GameObject collectItem;
    bool itemSetInitially;

    private void Awake() {
        //If we don't have an assigned item, complain, and make our objective uncompletable
        if(collectItem == null){
            Debug.Log("! - No item set! Please update objective with a key item to collect!");
            itemSetInitially = false;
        }else{
            itemSetInitially = true;
        }
        complete = false;
    }

    
    private void Update() {
        if(!complete)
            UpdateStatus();
    }

    //Interface method
    public void UpdateStatus(){
        complete = CheckForItemObtained();
    }

    //Check if object is still existing
    bool CheckForItemObtained(){
        if(collectItem == null && itemSetInitially){
            //We had an item but it's no longer existant
            return true;    
        }else{
            // either item is still there, or was never set
            return false;
        }
    }
}
