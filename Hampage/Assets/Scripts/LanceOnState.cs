using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* LanceOnState is a player state that has the players lance enabled
 */
public class LanceOnState : PlayerBaseState
{
    public GameObject playerObj;
    public Transform lance;
    public MeshRenderer lanceRenderer;

    // Inherit abstract player base state constructor
    public LanceOnState(PlayerStateManager player) : base("LanceOn", player ) { }

    /* EnterState() Sets the the visibility of the players lance on  
     */
    public override void EnterState(){
        // Set lance Visibility to On
        playerObj = GameObject.FindGameObjectWithTag("Player");
        lance = playerObj.transform.Find("Graphics/Lance?");
        lanceRenderer = lance.GetComponent<MeshRenderer>();
        lanceRenderer.enabled = true;
    }
    
    
}
