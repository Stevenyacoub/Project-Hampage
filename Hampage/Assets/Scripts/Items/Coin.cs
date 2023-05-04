using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    // Created by Giovanni Quevedo

    // Coin override: update Inventory's coin counter
    void OnTriggerEnter(Collider other){
        if(other.TryGetComponent<PlayerManager>(out PlayerManager playerManager)){
            playerManager.addCoin();
            Destroy(this.gameObject);
        }
    }

}
