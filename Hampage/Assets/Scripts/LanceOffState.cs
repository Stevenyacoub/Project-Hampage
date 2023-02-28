using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceOffState : PlayerBaseState
{
    public GameObject playerObj;
    public Transform lance;
    public MeshRenderer lanceRenderer;

    public LanceOffState(PlayerStateManager player) : base("LanceOff", player) { }

    public override void EnterState(PlayerStateManager player)
    {
       Debug.Log("Hello from EnterState of Lance Off");
        // Set visibility of lance off
        playerObj = GameObject.FindGameObjectWithTag("Player");
        lance = playerObj.transform.Find("Graphics/Lance?");
        lanceRenderer = lance.GetComponent<MeshRenderer>();
        lanceRenderer.enabled = false;
    }
    public override void UpdateState(PlayerStateManager player)
    {  
    }
    public override void ExitState(PlayerStateManager player)
    { 
    }
}
