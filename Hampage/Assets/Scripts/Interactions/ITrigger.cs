using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    // Created by Giovanni Quevedo
    // -- Triggers are puzzle elements that can activate others (activatables)
    Activatable activatable {get;set;}
    public bool activate();
    
}
