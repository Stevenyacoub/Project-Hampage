using UnityEngine;

public class Escape : MonoBehaviour, IObjective
{
    // Created by Giovanni Quevedo
    // Objective stipulating a player must reach the exit
    [field: SerializeField]
    public bool complete { get; set; }

    private void Awake() {
        // We update our status on awake (since we're true as long as the exit is reached)
        UpdateStatus();
    }

    // Called when a condition a
    public void UpdateStatus(){
        complete = true;
    }
}