using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceOnState : PlayerBaseState
{
    public GameObject playerObj;
    public Transform lance;
    public MeshRenderer lanceRenderer;

    public LanceOnState(PlayerStateManager player) : base("LanceOn", player ) { }

    public override void EnterState(PlayerStateManager player){
        Debug.Log("Hello from EnterState of Lance On");
        // Set lance Visibility to On
        playerObj = GameObject.FindGameObjectWithTag("Player");
        lance = playerObj.transform.Find("Graphics/Lance?");
        lanceRenderer = lance.GetComponent<MeshRenderer>();
        lanceRenderer.enabled = true;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        //player.SwitchState(player.LanceOffState);
    }
    public override void ExitState(PlayerStateManager player)
    {
    }
    
}
