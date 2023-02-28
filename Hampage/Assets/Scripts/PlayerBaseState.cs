using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class PlayerBaseState 
{
    public string stateName;
    protected PlayerStateManager player;

    public PlayerBaseState(string stateName, PlayerStateManager player) { 
        this.stateName = stateName;
        this.player = player;

    }

    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    public abstract void ExitState(PlayerStateManager player);

}
