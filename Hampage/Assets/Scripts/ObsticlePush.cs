using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticlePush : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rigidBody = hit.collider.attachedRigidbody;

        if (rigidBody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidBody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);


        }
    }
}

