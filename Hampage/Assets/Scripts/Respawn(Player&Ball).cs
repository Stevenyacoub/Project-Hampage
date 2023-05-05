using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPandB : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnAnchor;
    [SerializeField] private Transform Ball;
    [SerializeField] private Transform BallAnchor;
    //HamsterBallMovement HbM;
    public Rigidbody rb;


    void OnTriggerEnter(Collider other) 
    {
        //Player.isKinematic = true;
        Player.transform.position = respawnAnchor.transform.position;
        Ball.transform.position = BallAnchor.transform.position;
        Physics.SyncTransforms();
        //Player.isKinematic = false;
        //HbM.velocity = Vector3.zero;
    }
}
