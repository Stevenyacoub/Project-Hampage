using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 5;
    public DeathScreen deathScreen; //Alan
    UISystem ui;
    ControllerCharacter charControls;

    void Start() {
        charControls = GameManager.staticPlayer.GetComponent<ControllerCharacter>();
    }

    private bool isDead; //Alan

    public void AddHealth(float health)
    {
        Debug.Log("Ham healed! Current Health: " + this.health);
        this.health += health;
    }

    public void DecreaseHealth(float health)
    {
        //checks if player dies and is not already dead
        if (this.health <= 0 && !isDead) //modified by Alan
        {
            //player is no longer dead
            isDead = true; //Alan
            //activates death screen
            deathScreen.gameOver(); //Alan
            Debug.Log("Ham is dead!");
            
            //Don't destroy, but disable character
            //Destroy(gameObject);
            charControls.enabled = false;
            
        }
        else
        {
            Debug.Log("Ham took damage! Current Health: " + this.health);
            this.health -= health;
            
        }

        ui.UpdateHealthCounter(this.health);
    }

    public void setUpWithUI(UISystem ui){
        this.ui = ui;
    }

    
}
