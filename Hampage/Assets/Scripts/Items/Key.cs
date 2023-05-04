using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    // Created by Giovanni Quevedo

    // Key override: update Inventory's coin counter
    void OnTriggerEnter(Collider other){
        if(other.TryGetComponent<PlayerManager>(out PlayerManager playerManager)){
            playerManager.addKey();
            Destroy(this.gameObject);
        }
    }

}
