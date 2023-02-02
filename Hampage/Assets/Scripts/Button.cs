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
    

    public PlayerManager playerManager;
    BlockMovement blockMovement;

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
        blockMovement = FindObjectOfType<BlockMovement>();
        blockMovement.OperateDoor();
        return true;
    }
    
    // Unity Methods

    //Awake gets called before the scene is finished loading (even before Start()!)
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
    }

    //When a rigidbody collides with the objects collider, this will be called
    // NOTE: Collider must have isTrigger set to true
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !registered){
            playerManager.registerInteractable(this);
        }
    }
    //Called when rigidbody leaves the collider
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player" && registered){
            playerManager.unregisterInteractable(this);
        }
    }

}
