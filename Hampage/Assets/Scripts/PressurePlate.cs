using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private MeshRenderer colorChange;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Moveable")
        {
            Debug.Log("Pressure Plate Triggered");
            float xdistance = Vector3.Distance(transform.position, other.transform.position);
            Debug.Log(" X Distance:" + xdistance);
            colorChange = GetComponent<MeshRenderer>();
            if(colorChange != null)
            {
                colorChange.material.color = Color.blue;
            }
            
            
        }
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
