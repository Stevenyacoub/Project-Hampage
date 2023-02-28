using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    
    // Calls the abstract state class as a place holder for the players current state
    PlayerBaseState currentState;
    // instantiate lance on state
    public LanceOnState lanceOn;
    // instantiate lance off state
    public LanceOffState lanceOff;


    void Awake() {
        
        lanceOn = new LanceOnState(this);
        lanceOff = new LanceOffState(this);
        
    }
    void Start(){
        currentState = lanceOn;
        currentState.EnterState(this);
    }
    void Update(){
        currentState.UpdateState(this);
    }
    public void SwitchState(/*PlayerBaseState state*/) {
        if (currentState.stateName == "LanceOn")
        {
            currentState = lanceOff;
            currentState.EnterState(this);
        }
        else {
            currentState = lanceOn;
            currentState.EnterState(this);
        }
    }
    public string GetStateName() {
        return currentState.stateName;
    }

    private void OnGUI() {
        string content = currentState != null ? currentState.stateName : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
