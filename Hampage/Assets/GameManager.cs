using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Created by Giovanni Quevedo

    // Class to manage level state & progression

    List<IObjective> levelObjectives;
    
    private void Awake() {
        //Get all objectives on our gameobject
        levelObjectives = transform.GetComponentsInChildren<IObjective>().ToList();
    }

    // Called when a player steps on a level exit
    public void CheckForExit(){
        bool canLeave;
        
        if(levelObjectives.Count == 0){
            canLeave = false;
            Debug.Log("! - No level objectives set! Please add a child with an objective to the GameManager!");
        }else{
            canLeave = true;
        }

        foreach (var obj in levelObjectives)
        {
            if(obj.complete != true)
                canLeave = false;
        }

        if(canLeave){
            Debug.Log("Whoosh!");
            NextLevel();
        }else{
            Debug.Log("Can't leave the scene - " + (levelObjectives.Count == 0 ? " there are no objectives set!" : " there are unfinished objectives!"));
        }

    }

    void NextLevel(){
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if( sceneCount > nextSceneIndex)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            Debug.Log("Scene index of " + nextSceneIndex + " out ouf bounds, build only has " + sceneCount + " scenes!");
    }
   
}
