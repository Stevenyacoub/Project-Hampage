using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttack : MonoBehaviour, AttackStrategy
{

    // Created by Steven Yacoub

    public int damageAmount = 1;
    private Vector3 knock;
     // Value to modify y movement in knockback
    float knockbackModifier = -0.7f;

    ControllerCharacter playerController;
    void Start() {
        playerController = GameManager.staticPlayer.GetComponent<ControllerCharacter>();
    }

    public void performAttack()
    {
        throw new System.NotImplementedException();
    }

    // Hurt the player if he touches us
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            HurtPlayer(col.gameObject);
        }
    }

   

    // Hurt the player if he enters our hitbox 
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            HurtPlayer(col.gameObject);
        }
    }

    private void HurtPlayer(GameObject player){
        knock = playerController.transform.position - transform.position;

        // Set the up direction to 0 and keep the x and z the same
        knock.y = knockbackModifier;
        Vector3.Normalize(knock);

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        health.DecreaseHealth(damageAmount);
        playerController.Knockback(playerController.transform.position - transform.position);
        playerController.Knockback(knock);
    }


}

