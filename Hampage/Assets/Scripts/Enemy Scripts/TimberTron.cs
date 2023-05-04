using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimberTron : MonoBehaviour, Damageable
{
    public float healthPoints = 2;
    public Item[] possibleDrops;


    public void takeDamage(float damage)
    {
        if (healthPoints <= 0)
        {
            Debug.Log("TimberTron is dead!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("TimberTron took damage! Current Health: " + healthPoints);
            healthPoints -= damage;
        }
    }


}
