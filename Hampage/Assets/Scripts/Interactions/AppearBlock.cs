using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearBlock : Activatable
{
    // Created by Giovanni Quevedo
    // -- AppearBlock is a puzzle element that appears when activated

    private bool activated;
    private MeshRenderer cubeMesh;

    // Public method called when an ITrigger item is triggered
    public override bool startActivation()
    {
        toggleVis();
        return true;
    }

    // Start is called before the first frame update
    void Awake()
    {
        // Set up our variables on startup
        cubeMesh = GetComponent<MeshRenderer>();
        activated = false;
        cubeMesh.enabled = false;
    }

    // Toggle block's appearance
    void toggleVis(){
        if(!activated){
            cubeMesh.enabled = true;
            activated = true;
        }else{
            cubeMesh.enabled = false;
            activated = false;
        }
    }


}
