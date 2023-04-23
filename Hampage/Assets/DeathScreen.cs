using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    //makes a public instance
    public GameObject gameOverUI;

    //disables cursor when in play mode
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //activates cursor when death screen in activated, disables when not
    void Update()
    {
        if(gameOverUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //activates death screen when called
    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    //gets current scene and reloads it
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart");
    }

    //loads main menu scene
    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Main Menu");
    }

    //quits game
    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
