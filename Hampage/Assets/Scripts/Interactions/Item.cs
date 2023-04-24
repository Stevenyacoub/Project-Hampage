using UnityEngine;

public class Item : MonoBehaviour
{
    
    // Created by Giovanni Quevedo
    // -- Items are things that can be obtained by the player
    // - moves the item up and 
    // -- They are children of the Interactable class, and destroy themselves when interacted with

    protected Vector3 location;

    // For item bobbing & rotation
    protected float spinSpeed = 120f;
    protected float startHeight;

    // Gain references to our player and it's classes on startup
    void Start() {
        // Legacy - shows how to obtain the player from the static gamemanager
        // GameObject player = GameManager.staticPlayer;       
        location = transform.position;

        startHeight = transform.position.y;
    }

    void Update()
    {
        //Rotates left using speed
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

        //Bobs item
        transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(startHeight - 0.15f, startHeight + 0.15f, Mathf.PingPong(Time.time * 0.8f, 1f)), transform.position.z);
    }

    // Updates player inventory, then destroys object
    void OnTriggerEnter(Collider other){
        Debug.Log("Collided -");
        if(other.TryGetComponent<PlayerManager>(out PlayerManager playerManager)){
             Debug.Log("Player collis-");
            playerManager.addToInventory(this);
            Destroy(this.gameObject);
        }
    }

}
