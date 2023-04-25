using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BreakWall : MonoBehaviour
{
    // Destroys an object on collision specifically with the HamsterBall
    private void OnCollisionEnter(Collision collision){
        // Only disable the wall if it comes in contact with the hamster ball
        if(collision.collider.CompareTag("HamsterBall")){ 
           gameObject.SetActive(false);
        }
    }
}
