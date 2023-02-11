using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttack : MonoBehaviour, AttackStrategy
{
    public int damageModifier = 1;
    //ControllerCharacter player;

    public void performAttack()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = col.gameObject.GetComponent<PlayerHealth>();
            health.DecreaseHealth(damageModifier);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = col.gameObject.GetComponent<PlayerHealth>();
            health.DecreaseHealth(damageModifier);
            //player.Knockback(player.transform.position - transform.position);

        }
    }
}
