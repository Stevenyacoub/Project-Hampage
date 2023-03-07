using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttack : MonoBehaviour, AttackStrategy
{
    public int damageModifier = 1;
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
            health.DecreaseHealth(damageModifier);
            Debug.Log("Collision finally worked omfg!!!!!");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == contr.player)
        {
            knock = col.transform.position - contr.player.transform.position;
            //knock = knock.normalized;
            PlayerHealth health = col.gameObject.GetComponent<PlayerHealth>();
            health.DecreaseHealth(damageModifier);
            contr.Knockback(knock);         
        }
    }
}
