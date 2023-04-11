using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Player State Manager is part of the player gameObject that monitors the states
 *  of the player object. 
 */
public class PlayerStateManager : MonoBehaviour
{
    
    // Calls the abstract state class as a place holder for the players current state
    PlayerBaseState currentState;
    // instantiate lance on state
    public LanceOnState lanceOn;
    // instantiate lance off state
    public LanceOffState lanceOff;

    void Awake() {
        // Instantiate the states
        lanceOn = new LanceOnState(this);
        lanceOff = new LanceOffState(this);
    }
    void Start(){
        // Set the start state as lanceOn
        currentState = lanceOn;
        currentState.EnterState();
    }
   
    /* SwitchState() checks what state the player is currently in and switches it to the opposite state.
     * Since there are only two states there is only two options, but can be modified if we decide to add
     * more. 
     */
    public void SwitchState() {
        // If we are in LanceOn state then switch to lanceOff, otherwise switch to lanceOn state
        if (currentState.stateName == "LanceOn")
        {
            currentState = lanceOff;
            currentState.EnterState();
        }
        else {
            currentState = lanceOn;
            currentState.EnterState();
        }
    }
    // Returns the name of the state the player is currently in
    public string GetStateName() {
        return currentState.stateName;
    }

}
