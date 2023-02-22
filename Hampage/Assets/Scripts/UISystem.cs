using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    // Created by Giovanni Quevedo

    // Concerned with managing the HUD for the player

    [SerializeField]
    GameObject timesUpScreen;

    //Display "Time's Up!" Screen when timer runs out
    public void ShowTimesUp(){
        timesUpScreen.SetActive(true);
    }

    private void Awake() {
        
    }
}
