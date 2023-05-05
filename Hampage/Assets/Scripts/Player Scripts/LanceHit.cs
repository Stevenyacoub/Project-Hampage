using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceHit : MonoBehaviour
{
    public float damageAmount = 1;

    // Shouldn't be necessary, but testing just in case

    // private void OnCollisionEnter(Collision col)
    // {
    //     Debug.Log("Collis");

    //     if (col.gameObject.tag == "Enemy")
    //     {
    //         Wormy health = col.gameObject.GetComponent<Wormy>();
    //         health.takeDamage(damageModifier);
    //     }
    // }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Trigger");

        if (col.gameObject.tag == "Enemy")
        {
            Damageable health = col.gameObject.GetComponent<Damageable>();
            health.takeDamage(damageAmount);            
            //enemy knockback
        }
    }
}
