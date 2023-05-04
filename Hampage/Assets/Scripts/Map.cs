using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

/* Minimap is a script attached to the minimap camera. The camera follows the player from above and projects what it sees into a render on the Canvas.
 */

// Created by Samatha Reyes

public class Map : MonoBehaviour
{
    [SerializeField]
    private GameObject map;
    [SerializeField]
    private Camera mapCamera;
    [SerializeField]
    private RawImage mapRender;

    public static DefaultInputActions defaultInput;

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
        // Define movement using default input system
        defaultInput = new DefaultInputActions();
        // add callbacks to when input is performed
        // - There are 3 states an input can be in: Started (Button Down), performed (button held), and cancelled (released)
        defaultInput.UI.Navigate.started += OnNavigate;
        defaultInput.UI.Navigate.performed += OnNavigate;
        defaultInput.UI.ScrollWheel.started += OnScrollWheel;
        defaultInput.UI.ScrollWheel.performed += OnScrollWheel;
        // Enable the input
        defaultInput.Enable();
        // usually, we disable when the gameobject is gone
        // since we intend to disable the map frequently, we instead moved this call to the UI system
        // see UISystem.cs


        // Set the size "zoom" to be 12
        mapCamera.orthographicSize = 12f;
        // Get the position of the camera
        cameraPosition = mapCamera.transform;
    }

    // ! - Callback - gets called from the DefaultInputActions when conditions arise (see Awake)
    // OnNavigate is located in the defaultUiContols and relates to WASD, arrow keys, and left joystick movement
    public void OnNavigate(InputAction.CallbackContext value) {
        // Get the movement from the keypresses
        camMovement = value.ReadValue<Vector2>();
        // translate the camera based on the keypresses and the movespeed of the map nav
        cameraPosition.Translate(camMovement * moveSpeed);
    }

    // ! - Callback - gets called from the DefaultInputActions when conditions arise (see Awake)
    // OnScrollWheel is locates in the defaultUIControls and relates to the mouse scroll wheel
    public void OnScrollWheel(InputAction.CallbackContext value) {
        // get the movement from the scroll wheel interaction
        zoom = value.ReadValue<Vector2>();
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
