using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBox : MonoBehaviour
{
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called when a rigidbody enters the collider
    private void OnTriggerEnter(Collider other)
    {
        //Try to get an interactable out of the collider
        // !! - this only works if the collider and the script are on the same gameobject! Not a child or parent
        if(other.TryGetComponent<IInteractable>(out IInteractable interactable)){
            if(!interactable.registered){
               playerManager.registerInteractable(interactable);
            }
        }
    }
    //Called when rigidbody leaves the collider
    private void OnTriggerExit(Collider other) {
        if(other.TryGetComponent<IInteractable>(out IInteractable interactable)){
            if(interactable.registered){
               playerManager.unregisterInteractable(interactable);
            }
        }
    }
}
