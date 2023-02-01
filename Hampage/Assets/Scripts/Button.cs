using UnityEngine;

public class Button : MonoBehaviour, IInteractable, ITrigger
{
    //We have a private member that our accessor interfaces with
    // This is done so we can use the unity inspector to assign Activatables to triggerables
    [SerializeField]
    private Activatable active;
    public Canvas interactUI;
    public Camera mainCam;

    public Activatable activatable {
        get {return active;} 
        set {active = value;} 
    }

    public bool registered { get; set; }
    bool UIShown;
    
    void Awake() {
        interactUI.enabled = false;
        UIShown = false;
    }

    void Update() {
        if(registered & !UIShown){
            interactUI.enabled = true;
            UIShown = true;
        }else if(!registered & UIShown){
            interactUI.enabled = false;
            UIShown = false;
        }

        if(UIShown){
            interactUI.transform.LookAt(mainCam.transform);
        }
    }

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
