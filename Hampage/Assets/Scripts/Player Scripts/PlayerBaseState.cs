using System.Runtime.CompilerServices;
using UnityEngine;

/* Player Base State is an abstract class for player based states
 * Each state will have a name, and is monitored through a player state manager.
 */
public abstract class PlayerBaseState 
{
    public string stateName;
    protected PlayerStateManager player;

    // Base Class Constructor
    public PlayerBaseState(string stateName, PlayerStateManager player) { 
        this.stateName = stateName;
        this.player = player;

    }
    // EnterState() abstract method
    public abstract void EnterState();
    

}
