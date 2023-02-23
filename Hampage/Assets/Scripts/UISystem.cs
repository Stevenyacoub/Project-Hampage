using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    // Created by Giovanni Quevedo

    // Concerned with managing the HUD for the player

    [SerializeField]
    GameObject timesUpScreen;
    [SerializeField]
    UnityEngine.UI.Button restartButton;
    [SerializeField]
    UnityEngine.UI.Button mainMenuButton;

    public GameManager gameMan;

    //Display "Time's Up!" Screen when timer runs out
    public void ShowTimesUp(){
        timesUpScreen.SetActive(true);
    }

    // Sets the game manager, and then uses it to assign the button functionality
    public bool SetUpWithManager(GameManager gameManager){
        this.gameMan = gameManager;
        AssignButtons();
        return true;
    }

    // Assigns buttons their respective functionalities from gamemanager
    void AssignButtons(){
        //Assign buttons for Time-Up screen
        AssignTimeUpButtons();
    }

    void AssignTimeUpButtons(){
        restartButton.onClick.AddListener(gameMan.RestartLevel);
        mainMenuButton.onClick.AddListener(gameMan.MainMenu);
    }
}
