using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public GameObject player { get; set; }
    public bool registered { get; set; }
    float radius = 3f;
    

    public void checkForRadius()
    {
        //Check player location, if in raidus register ourself in it's interactables
            float distance = Mathf.Sqrt(Mathf.Pow((player.transform.position.x - transform.position.x),2) + Mathf.Pow((player.transform.position.z - transform.position.z),2));

        if(!registered){
            if(distance <= radius && (transform.position.y - player.transform.position.y <= 1 || transform.position.y - player.transform.position.y >= -1)){
                player.GetComponent<ControllerCharacter>().registerInteractable(this);
                // Player will change registered status once sucessful
            }
        }else{
            if(distance > radius || (transform.position.y - player.transform.position.y > 1 || transform.position.y - player.transform.position.y < -1)){
                player.GetComponent<ControllerCharacter>().unregisterInteractable(this);
                // Player will change registered status once sucessful
            }
        }
    }

    public bool performAction()
    {
        Debug.Log("Button pressed!");
        return true;
    }

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       checkForRadius(); 
    }
}
