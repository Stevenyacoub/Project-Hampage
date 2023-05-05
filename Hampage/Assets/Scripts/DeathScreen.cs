using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    //makes a public instance
    public GameObject gameOverUI;

    //activates cursor when death screen in activated, disables when not
    void Update()
    {
        // if(gameOverUI.activeInHierarchy)
        // {
        //     Cursor.visible = true;
        //     Cursor.lockState = CursorLockMode.None;
        // }
        // else
        // {
        //     Cursor.visible = false;
        //     Cursor.lockState = CursorLockMode.Locked;
        // }
    }

    //activates death screen when called
    public void gameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverUI.SetActive(true);
    }

}
