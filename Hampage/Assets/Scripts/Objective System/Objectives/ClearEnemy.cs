using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEnemy : MonoBehaviour, IObjective
{
    // Created by Giovanni Quevedo
    // Objective stipulating an enemy (or Damagable) needs to be emilinated
    [field: SerializeField]
    public bool complete { get; set; }

    [SerializeField]
    GameObject enemyGameobject;
    Damageable clearEnemy;
    bool enemySetInitially;

    private void Awake() {
        //If we don't have an assigned enemy, complain, and make our objective uncompletable
        if(enemyGameobject == null){
            Debug.Log("! - No enemy set! Please update objective with a Damagable to clear!");
            enemySetInitially = false;
        }else{
            enemySetInitially = true;
            clearEnemy = enemyGameobject.GetComponent<Damageable>();
        }
        complete = false;
    }

    
    private void Update() {
        if(!complete)
            UpdateStatus();
    }

    //Interface method
    public void UpdateStatus(){
        complete = CheckForDamagableCleared();
    }

    //Check if object is still existing
    bool CheckForDamagableCleared(){
        if(enemyGameobject == null && enemySetInitially){
            //We had an item but it's no longer existant
            return true;    
        }else{
            // either item is still there, or was never set
            return false;
        }
    }
}
