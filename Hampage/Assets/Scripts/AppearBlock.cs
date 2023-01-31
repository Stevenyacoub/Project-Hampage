using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearBlock : Activatable
{
    private bool activated;
    private MeshRenderer cubeMesh;

    public override bool startActivation()
    {
        toggleVis();
        return true;
    }

    // Start is called before the first frame update
    void Awake()
    {
        cubeMesh = GetComponent<MeshRenderer>();
        activated = false;
        cubeMesh.enabled = false;
    }

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
