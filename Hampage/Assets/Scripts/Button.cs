using UnityEngine;

public class Button : MonoBehaviour, IInteractable, ITrigger
{
    //We have a private member that our accessor interfaces with
    // This is done so we can use the unity inspector to assign Activatables to triggerables
    [SerializeField]
    private Activatable active;

    public Activatable activatable {
        get {return active;} 
        set {active = value;} 
    }

    public bool registered { get; set; }
    

    public bool activate(){
        if(active != null){
            return active.startActivation(); 
        }else{
            Debug.Log("No activatable to activate!");
            return false;
        }
    }

    public bool performAction(){
        //Since this button is also a trigger, it's perform action is to activate its activatable

        Debug.Log("Button pressed!");
        return activate();
    }

}
