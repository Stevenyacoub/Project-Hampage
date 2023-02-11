using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceHit : MonoBehaviour
{
    public float damageModifier = 1;
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Wormy health = col.gameObject.GetComponent<Wormy>();
            health.takeDamage(damageModifier);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Wormy health = col.gameObject.GetComponent<Wormy>();
            health.takeDamage(damageModifier);            
            //enemy knockback
        }
    }
}
