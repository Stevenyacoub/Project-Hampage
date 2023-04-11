using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{

    // Created by Giovanni Quevedo
    // Gameobject that triggers a transition to the next level if objectives are met

    GameManager gameMan;
    // Called when a gameobject enters the trigger
    // if the object is a player, tries to exit the level
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if(other.tag == "Player")
            gameMan.CheckForExit();
    }

    void Awake() {
        gameMan = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(!gameMan){
            Debug.Log("! - No GameManager detected, please set one up!");
        }
    }
}
