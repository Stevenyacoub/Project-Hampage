using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnAnchor;

    void OnTriggerEnter(Collider other) 
    {
        Player.transform.position = respawnAnchor.transform.position;
        Physics.SyncTransforms();
    }
}
