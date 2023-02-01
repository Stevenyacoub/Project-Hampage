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
            
            //We got an interactable!
            //Now try to see if we can cast a ray to it (meaning nothing is in the way)
            
            //Shoot a ray to the object
            if(Physics.Raycast(transform.position, (other.transform.position - transform.position), out RaycastHit hit)){

                //See if the hit object also has an interactable (ignore if not)
                if(hit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable hitInteractable)){

                    //If we can reach it and it's not registered
                    if(hitInteractable.Equals(interactable) && !interactable.registered){
                        playerManager.registerInteractable(interactable);
                    }
                }   
            }  
        }
    }

    //Called while rigidbody is still in collider
    private void OnTriggerStay(Collider other) {
        if(other.TryGetComponent<IInteractable>(out IInteractable interactable)){
            
            //Shoot a ray to the object
            if(Physics.Raycast(transform.position, (other.transform.position - transform.position), out RaycastHit hit)){

                //See if the hit object also has an interactable (ignore if not)
                if(hit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable hitInteractable)){

                    //If we can reach it and it's not registered
                    if(hitInteractable.Equals(interactable) && !interactable.registered){
                        playerManager.registerInteractable(interactable);
                    // Or if we can't reach but it is...
                    }else if(!hitInteractable.Equals(interactable) && interactable.registered){
                        playerManager.unregisterInteractable(interactable);
                    }
                }   
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
