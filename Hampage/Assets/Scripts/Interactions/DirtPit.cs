using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPit : Interactable
{
    // Created by Giovanni Quevedo
    // -- Dirt pits act like tunnels, allowing a player to enter one side 
    //  - and dig to the next

    public DirtPit partnerPit;
    public GameObject player;
    private IEnumerator waitForSpawn;
    public InteractBox interactBox;

    // Variables for tunneling
    float startTime;
    bool doAnimate = false;
    Vector3 endPos;
    // Adjustable time for how long tunnel takes traverse
    public float tunnelTime = 1.5f;
    
    // When interacted, tries to tunnel player to a partner. Returns false if none exist
    public override bool performAction()
    {
        if(!partnerPit){
            Debug.Log("Can't activate, no partner!");
            return false;
        }else{
            return TunnelPlayer();
        }
    }

    // Awake is called before the first frame update
    // -- Set our variables, and update our partner pit (if it existss)
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactBox = player.transform.Find("InteractBox").GetComponent<InteractBox>();
        // If our partner was assigned via inspector
        if(partnerPit){
            //If they don't have a reference to us, set it
            if(!partnerPit.partnerPit){
                partnerPit.partnerPit = this;
            }
        }else{
            // TO-DO: Test if this is necessary
            partnerPit = null;
        }
    }

    // Update is called once per frame
    // -- Handle the camera movement (animation) if it's enabled 
    void Update()
    {
        if(doAnimate)
            AnimateTunneling(endPos);
    }

    // Tunnel the player to the other side
    bool TunnelPlayer(){

        // Find end position
        endPos = partnerPit.transform.position + new Vector3(0,1,0);

        // Set an instance of the Corutine up to teleport player and 
        waitForSpawn = WaitForSpawn(tunnelTime,endPos);

        // Disable Player's game object to move it freely
        player.SetActive(false);

        // Start the corutine to re-activate player after tunneling is done
        StartCoroutine(waitForSpawn);

        //Set flag to start animating our transition
        startTime = Time.time;
        doAnimate = true;

        //Once we're done tunneling, the player's interactbox won't auto update
        //unsubscribe ourselves from their interact list
        interactBox.UnregisterInteractable(this);
        
        return true;
    }




    // WIP
    private void AnimateTunneling(Vector3 endPos){
        
        float amountComplete = (Time.time - startTime) / tunnelTime;
        player.transform.position = Vector3.Slerp(transform.position, endPos, amountComplete);

    }

    // WIP - should be able to also teleport player successfully
    private IEnumerator WaitForSpawn(float waitTime, Vector3 endPos)
    {
        yield return new WaitForSeconds(waitTime - 0.5f);
        doAnimate = false;
        player.transform.position = endPos;
        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
    }


    
}
