using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormy : MonoBehaviour, Damageable
{
    public float healthPoints = 2;
    public Item[] possibleDrops;


    public void takeDamage(float damage)
    {
        if (healthPoints <= 0)
        {
            Debug.Log("Worm is dead!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Worm took damage! Current Health: " + healthPoints);
            healthPoints -= damage;
        }
    }


}
