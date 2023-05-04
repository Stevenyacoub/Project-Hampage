using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* LanceOnState is a player state that has the players lance enabled
 */
public class LanceOffState : PlayerBaseState
{
    public GameObject playerObj;
    public Transform lance;
    public MeshRenderer lanceRenderer;

    // Inherit abstract player base state constructor
    public LanceOffState(PlayerStateManager player) : base("LanceOff", player) { }

    /* EnterState() Sets the the visibility of the players lance Off  
     */
    public override void EnterState()
    {
        // Set visibility of lance off
        playerObj = GameObject.FindGameObjectWithTag("Player");
        lance = playerObj.transform.Find("Graphics/Lance?");
        lanceRenderer = lance.GetComponent<MeshRenderer>();
        lanceRenderer.enabled = false;
    }
   
}
