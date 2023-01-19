using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy<T> where T : Enemy
{
    public GameObject obj;
    public T script;

    public Enemy(string name)
    {
        obj = new GameObject(name);
        script = obj.AddComponent<T>();
    }
}

public abstract class Enemy : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider collision;
    public float enemyHealth;

    protected abstract void takeDamage(float damageAmount);

    private void InitEnemy()
    {
        //Base components
        rb = gameObject.AddComponent<Rigidbody>();
        collision = gameObject.AddComponent<BoxCollider>();
        enemyHealth = 1;
    }
}
