using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

/* Minimap is a script attached to the minimap camera. The camera follows the player from above and projects what it sees into a render on the Canvas.
 */
public class Map : MonoBehaviour
{
    [SerializeField]
    private GameObject map;
    [SerializeField]
    private Camera mapCamera;
    [SerializeField]
    private RawImage mapRender;

    [SerializeField]
    private InputActionAsset defaultUIControls;

    // Min and max zoom values for zoom function
    public float minZoom = 8f, maxZoom = 32f;
    // moveSpeed of the map navigation
    public float moveSpeed = .001f;

    //The transform of the map camera
    Transform cameraPosition;

    // variables to hold the return values from the input system
    private Vector3 camMovement;
    private Vector2 zoom;

    // On Awake we want to instantiate cameraPosition and camera size
    void Awake()
    {
        // Set the size "zoom" to be 12
        mapCamera.orthographicSize = 12f;
        // Get the position of the camera
        cameraPosition = mapCamera.transform;
        defaultUIControls.Enable();
    }

    // Update() checks to see if the pause menu is open or not. If it is, then we want to enable
    // the UI controls for the player. If not paused, then the Ui contols are not being used and are disabled
    void Update()
    {
        // TO-DO: add back 

        // if (UISystem.isPaused)
        // {
        //     defaultUIControls.Enable();
        // }
        // else {
        //     defaultUIControls.Disable();
        // }
    }

    // OnNavigate is located in the defaultUiContols and relates to WASD, arrow keys, and left joystick movement
    private void OnNavigate(InputValue value) {
        // Get the movement from the keypresses
        camMovement = value.Get<Vector2>();
        // translate the camera based on the keypresses and the movespeed of the map nav
        cameraPosition.Translate(camMovement * moveSpeed);
    }

    // OnScrollWheel is locates in the defaultUIControls and relates to the mouse scroll wheel
    private void OnScrollWheel(InputValue value) {
        // get the movement from the scroll wheel interaction
        zoom = value.Get<Vector2>();
        // If the scroll wheel is moving in then we want to zoom in
        if (zoom.y > 0)
        {
            ZoomIn();
        }
        // if the scroll wheel is moving out then we want to zoom out
        if (zoom.y < 0)
        {
            ZoomOut();
        }
    }
    // ZoomIn() changes the orthographicSize of the map by decreasing its value up until minZoom
    public void ZoomIn() {
        mapCamera.orthographicSize = Mathf.Clamp(mapCamera.orthographicSize -= .5f, minZoom, maxZoom);
    }
    // ZoomIn() changes the orthographicSize of the map by increasing its value up until maxZoom
    public void ZoomOut(){
        mapCamera.orthographicSize = Mathf.Clamp(mapCamera.orthographicSize += .5f, minZoom, maxZoom);
    }

}
