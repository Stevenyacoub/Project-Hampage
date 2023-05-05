using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnAnchor;
    //HamsterBallMovement HbM;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other) 
    {
        //Player.isKinematic = true;
        Player.transform.position = respawnAnchor.transform.position;
        Physics.SyncTransforms();
        //Player.isKinematic = false;
        //HbM.velocity = Vector3.zero;
    }
}