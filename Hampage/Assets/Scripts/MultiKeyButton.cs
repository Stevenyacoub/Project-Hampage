using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MultiKeyButton : Interactable, ITrigger
{
    // Original Button/Objectives by Giovanni and Devin, Modified by Samantha 
    // This is a modified version of the buttons from our game that expands its use to incorporate multi check
    // activation
    // MultiKeyButtons are ITriggers that can be interacted with only if all conditions (objectives) are met 
    // For the ITrigger:
    [SerializeField]
    private Activatable active;

    // Objectives for level (need to be complete to activate the button), for this case it is dependent on the keys collected
    List<IObjective> KeysCollected;

    // Accessors for Activatable
    public Activatable activatable
    {
        get { return active; }
        set { active = value; }
    }

    private void Awake() {
        //Get all objectives on our gameobject
        KeysCollected = transform.GetComponentsInChildren<IObjective>().ToList();
    }

    // Activate our activatable if we have one, if not return false
    public bool activate()
    {
        if (active != null)
        {   
            // Unlike a regular activatable, we want to check that all conditions have been met( aka all Keys have
            // been collected). If not all keys have been collected then do not start the activation
            if (CheckForKeys())
            {
                return active.startActivation();
            }
            else{
                Debug.Log("Not All keys have been collected!");
                return false;
            }
        }
        else
        {
            Debug.Log("No activatable to activate!");
            return false;
        }
    }

    // Button's unique implementation, activates it's activatable and notifies via console
    public override bool performAction()
    {
        //Since this button is also a trigger, it's perform action is to activate its activatable
        Debug.Log("Button pressed!");
        return activate();
    }
    // Called when a player presses the button to activate it, this
    // checks if the player can activate the button, which can only happen if they have all the keys
    public bool CheckForKeys()
    {
        bool canActivate;
        // If no objectives exist, we can't leave
        if (KeysCollected.Count == 0)
        {
            canActivate = false;
            Debug.Log("!No keys set! Please add a child with an objective to the Button!");
        }
        else
        {
            canActivate = true;
        }
        // Make sure each of our objectives is complete
        foreach (var obj in KeysCollected)
        {
            if (obj.complete != true)
                canActivate = false;
        }
        // Return the value to determine whether to allow it or not
        return (canActivate);
    }
}
