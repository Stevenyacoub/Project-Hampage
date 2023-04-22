using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Created by Giovanni Quevedo

    // Class to manage level state & progression

    // Objectives for level (need to be complete to level up)
    List<IObjective> levelObjectives;
    // Reference to a UI system
    UISystem UI;
    public GameObject player;
    public static GameObject staticPlayer;


    public static GameManager instance;

    // Flag to stop level up (in case time runs out)
    public static bool timeUp = false;

    // Awake is called before the first frame update
    private void Awake() {
        //Get all objectives on our gameobject
        levelObjectives = transform.GetComponentsInChildren<IObjective>().ToList();        
        //Instantiate a HUD instance, and set it up (with ourself)
        UI = transform.Find("UISystem").GetComponent<UISystem>();
        UI.SetUpWithManager(this);
        instance = this;
        staticPlayer = player;

    }

    // Called when a player steps on a level exit
    // - checks if the player can leave (all objectives must be complete) and levels them up if they can
    public void CheckForExit(){
        bool canLeave;
        
        // If no objectives exist, we can't leave
        if(levelObjectives.Count == 0){
            canLeave = false;
            Debug.Log("! - No level objectives set! Please add a child with an objective to the GameManager!");
        }else{
            canLeave = true;
        }

        // Make sure each of our objectives is complete
        foreach (var obj in levelObjectives)
        {
            if(obj.complete != true)
                canLeave = false;
        }

        // Level up if we can else complain via print
        if(canLeave){
            Debug.Log("Whoosh!");
            NextLevel();
        }else{
            Debug.Log("Can't leave the scene - " + (levelObjectives.Count == 0 ? " there are no objectives set!" : " there are unfinished objectives!"));
        }

    }

    // Try to level up if we can
    void NextLevel(){
        if(!timeUp){
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            if( sceneCount > nextSceneIndex)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                Debug.Log("Scene index of " + nextSceneIndex + " out ouf bounds, build only has " + sceneCount + " scenes!");
        }else{
            Debug.Log("Can't level up due to time out!!!");
        }
    }

    // Restart current level
    public void RestartLevel(){
        Time.timeScale = 1f;
        UISystem.isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load main menu
    public void MainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    // Quit() dictates what the quit button does on click
    public void Quit() {
        // Closes the application
        Application.Quit();
    }

    // Show TimesUp ui, and stop taking input
    public void TimesUp(){
        // Let game manager know we cant move leave 
        timeUp = true;
        UI.ShowTimesUp();
        player.GetComponent<ControllerCharacter>().enabled = false;
    }

    public GameObject getPlayer(){
        return player;
    }
   
}
