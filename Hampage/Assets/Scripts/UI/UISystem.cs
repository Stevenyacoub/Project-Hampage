using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    // Pair programmed by Giovanni Quevedo and Samantha Reyes

    // Concerned with managing the HUD for the player

    [SerializeField]
    GameObject timesUpScreen;

    // Pause Menu variables:
    public GameObject pauseMenu;
    public static bool isPaused = false;

    // We need a player to reference, its transform, and an offset for the y axis on order to center the map on the player
    [SerializeField]
    private Camera mapCamera;
    [SerializeField]
    private GameObject player;
    Transform playerTransform;
    public float yOffset = 100f;

    // Text for HUD
    [SerializeField] 
    private TMP_Text coinHUD;
    [SerializeField] 
    private TMP_Text healthHUD;

    public PlayerManager playerMan;
    public PlayerHealth playerHealth;

    void Start() {
        // Get transform for map
        playerTransform = player.transform;
        // Lock cursor:
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Get player manager to get inventory info
        playerMan = GameManager.staticPlayer.GetComponent<PlayerManager>();
        // Have player manager have our instance so we can update UI
        playerMan.setUpWithUI(this);

        playerHealth = GameManager.staticPlayer.GetComponent<PlayerHealth>();
        // Same as playerman
        playerHealth.setUpWithUI(this);
    }

    // Called from GameManager
    public void UpdateCoinCounter(int numCoins){
        coinHUD.SetText("Coin: " + numCoins);
    }

    // Called from GameManager
    public void UpdateHealthCounter(float health){
        healthHUD.SetText("Health: " + health);
    }

    void Update() {
        // Once the player presses escape
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.timeUp)
        {
            // If the game is not paused then pause it, otherwise resume the game
            if (isPaused)
            {
                Resume();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                
            }
            else
            {
                Pause();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    // Pause() dictates what happens once the game is paused
    void Pause()
    {
        // Activate the pause menu
        pauseMenu.SetActive(true);
        // Positions the map camera to center on the player when we enter the pause menu
        
        // TO-DO: Pass this to map to control it's own transform
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

    //Display "Time's Up!" Screen when timer runs out
    public void ShowTimesUp(){
        timesUpScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }

    //Disables the map input if the UIsystem is ever disabled
    public virtual void OnDisable() {
        if(Map.defaultInput != null)
            Map.defaultInput.UI.Disable();
    }


}
