using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttack : MonoBehaviour, AttackStrategy
{
    public int amountDamage = 1;
    public ControllerCharacter contr;
    private Vector3 knock;

    public void performAttack()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == contr.player)
        {
            PlayerHealth health = col.gameObject.GetComponent<PlayerHealth>();
            health.DecreaseHealth(amountDamage);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == contr.player)
        {
            // Creates a knockback vector in the direction away from the enemy
            knock = contr.player.transform.position - transform.position;

            // Set the up direction to 0 and keep the x and z the same
            // float threshold = 0.0f;
            knock.y = -1.3f;
            Vector3.Normalize(knock);
            // if (knock.y > threshold){
            //     // Do something
            // }
            
            
            //knock = new Vector3(knock.x, 0.5f, knock.y);

            // have the player take damage, and trigger the controller's knockback

            Debug.Log("Initiated knockback, dir: "+ knock);
            Debug.Log("Attacker pos:" + transform.position);
            Debug.Log("Player Pos:" + contr.player.transform.position);
            PlayerHealth health = col.gameObject.GetComponent<PlayerHealth>();
            health.DecreaseHealth(amountDamage);
            contr.Knockback(knock);         
        }
    }
}
