using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject pauseMenu;
    public static bool isPaused = false;

    // We need a player to reference, its transform, and an offset for the y axis on order to center the map on the player
    [SerializeField]
    private Camera mapCamera;
    [SerializeField]
    private GameObject player;
    Transform playerTransform;
    public float yOffset = 100f;

    void Start()
    {
        playerTransform = player.transform;
        //On start we want the pause menu to be disabled
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Once the player presses escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is not paused then pause it, otherwise resume the game
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Pause() dictates what happens once the game is paused
    void Pause()
    {
        // Activate the pause menu
        pauseMenu.SetActive(true);
        // Positions the map camera to center on the player when we enter the pause menu
        mapCamera.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z);
        // Stop the game from playing, this includes enemies, moving objects, essentially everything
        Time.timeScale = 0f;
        // Disable the player controller since we do not need them to be moving around
        player.GetComponent<ControllerCharacter>().enabled = false;
        // Set isPaused bool to true
        isPaused = true;
    }

    // Resume() dictates what happens once the game is unpaused
    public void Resume()
    {
        // Deactivate the pause screen
        pauseMenu.SetActive(false);
        // Start time for the game again
        Time.timeScale = 1f;
        // Enable player movenment controls
        player.GetComponent<ControllerCharacter>().enabled = true;
        // Set isPaused bool to false
        isPaused = false;
    }

    // MainMenu() dictates what the main menu button does on click
    public void MainMenu() {
        // returns the game to the main menu
        SceneManager.LoadScene(0);
    }

    // Quit() dictates what the quit button does on click
    public void Quit() {
        // Closes the application
        Application.Quit();
    }
}
