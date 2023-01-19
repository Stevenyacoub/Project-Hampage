using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormy : Enemy
{
    private Object obj;
    public Rigidbody rb;
    public BoxCollider collision;
    public float enemyHealth;

    protected override void takeDamage(float damageAmount)
    {
        this.enemyHealth -= damageAmount;
        
        if(this.enemyHealth >= 0)
        {
            //death
            Object.Destroy(this.obj);
        }
        else
        {
            //knockback
        }
    }

    public void Update()
    {
        //if (this.collision.OnCollisionEnter("LanceHitbox"))
        //{
            //Call TakeDamage
        //}
    }

}
