using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    // Triggers are puzzle elements that can activate others (activatables)
    Activatable activatable {get;set;}
    public bool activate();
    
}
