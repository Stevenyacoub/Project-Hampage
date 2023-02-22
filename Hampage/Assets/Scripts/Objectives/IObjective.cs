using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjective
{

    // Created by Giovanni Quevedo

    // Interface for Objectives
    // - Objectives are objects representing a task that needs to 
    // - be completed before a player can exit the level

    // - Each level can have multiple objectives

    // Every objective has a complete flag 
    bool complete {get;set;}
    // Called differently dependant on implementation, updates the complete flag
    public void UpdateStatus();
    
}
