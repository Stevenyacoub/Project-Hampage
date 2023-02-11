using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 5;

    public void AddHealth(float health)
    {
        Debug.Log("Ham healed! Current Health: " + this.health);
        this.health += health;
    }

    public void DecreaseHealth(float health)
    {
        if (this.health <= 0)
        {
            Debug.Log("Ham is dead!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Ham took damage! Current Health: " + this.health);
            this.health -= health;
        }
    }
}
