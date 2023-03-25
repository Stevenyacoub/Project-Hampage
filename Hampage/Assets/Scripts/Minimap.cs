using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Minimap is a script attached to the minimap camera. The camera follows the player from above and projects what it sees into a render on the Canvas.
 */
public class Minimap : MonoBehaviour
{
    // We need a player to reference, its transform, and an offset for the y axis.
    public GameObject player;
    public Transform playerTransform;
    public float yOffset = 100f;

    public GameObject miniMap;
    public Camera MinimapCamera;

    public float minZoom = 8f;
    public float maxZoom = 18f;
    public float currZoom = 12f;
    

    // On Awake we want to instantiate the player variables
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        miniMap = GameObject.FindGameObjectWithTag("Map");
        MinimapCamera.orthographicSize = currZoom;
    }

    // Update() updates the camera per frame as the player moves about. It follows the position of the player exactly, except the y which needs to have offset 
    // placing the camera above the player. Additionally, there is a line for rotation if we want the minimap to rotate as the player does.
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            miniMap.SetActive(true);
            //player.GetComponent<ControllerCharacter>().enabled = false;
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z);
            if(Input.GetKey("down"))
            {
                if (MinimapCamera.orthographicSize < maxZoom)
                {
                    MinimapCamera.orthographicSize = currZoom + 1f;
                }
            }
            if (Input.GetKey("up"))
            {
                if (MinimapCamera.orthographicSize > minZoom)
                {
                    MinimapCamera.orthographicSize = currZoom - 1f;
                }
            }
            //transform.rotation = Quaternion.Euler(90f, playerTransform.eulerAngles.y, 0f);
        }
        else {
            miniMap.SetActive(false);
            //player.GetComponent<ControllerCharacter>().enabled = true;
        }
    }

    void Zoom() {
        if (Input.GetKey("down"))
        {
            if (MinimapCamera.orthographicSize < maxZoom)
            {
                MinimapCamera.orthographicSize = currZoom + 1f;
            }
        }
        if (Input.GetKey("up"))
        {
            if (MinimapCamera.orthographicSize > minZoom)
            {
                MinimapCamera.orthographicSize = currZoom - 1f;
            }
        }
    }
}
