using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : Activatable
{
    private bool activated;
    private BlockMovement bm;


    public override bool startActivation()
    {
        bm.OperateDoor();
        return true;
    }

    // Start is called before the first frame update
    void Awake()
    {
        bm = GetComponent<BlockMovement>();
    }

    


}