using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    /*Serialized field allowing us to add a waypointPath to the platform. The waypointPath is a collection of children waypoints
    that lay the "path" for the platforms movement*/
    [SerializeField]
    private Transform waypointPath;

    /*Serialized Field allowing us to change the speed at which the platform moves between points*/
    [SerializeField]
    private float speed;

    //Index of waypoint the path is moving towards
    private int destinationIndex;
    // Previous and Target Waypoints
    private Transform startWaypoint;
    private Transform destinationWaypoint;
    // Time wee expect it to take to travel between two points, and actual travel time
    private float expectedTime;
    private float travelTime;

    public GameObject gameController;
    public GameObject interactBox;


    // Start function runs when the scene runs so and we want the platform to move while we are in the scene 
    void Start()
    {
        updateWaypointPath();
        GameObject player = GameManager.staticPlayer;
        gameController = player.transform.parent.gameObject;

    }

    /*FixedUpdate (Unity Method)
    FixedUpdate is a method that runs several times per frame which is necessary since our platform moves through many frames during its path.
    Use Lerp to decide when to update to the next waypoint along the path. Lerp is a Vector3 method in unity that takes two positions 
    and an interpolant value in order to interpolate between the two points. If the interpolant is 0 then it is at hte start and if it is
    1 then it is at the destination. Use the start and destination waypoints as our points. Our interpolant is the total time we have spent moving
    divided by the amount of time we expect the travel to take. 
    */
    void FixedUpdate()
    {
        // Time.deltaTime returns the time of each frame. Since we reset it on the last update it shows our current travelTime.
        travelTime += Time.deltaTime;

        float interpolant = travelTime / expectedTime;
        transform.position = Vector3.Lerp(startWaypoint.position,destinationWaypoint.position, interpolant);

        // When our interpolant = 1 then we are at the destination and we need to start going to the next point
        if(interpolant >= 1){
            updateWaypointPath();
        }
    }

     /*OnTriggerEnter (Unity Method)
    If a player is detected on the platform make the player a child of the platform so that the player will move along with the
    platform rather than falling off. 
    */
    private void OnTriggerEnter(Collider player){
        if(player.CompareTag("Player")){
            player.transform.SetParent(transform);
        }

        // This method sets all entering triggers to be a child of waypoint, meaning it's also grabbing the interact box
    }
    /*OnTriggerExit (Unity Method)
    If the player leaves the platform then we need to remove them from being the platforms child object
    */
    private void OnTriggerExit(Collider player) {
        if(player.CompareTag("Player")){
            player.transform.SetParent(gameController.transform);
        }
    }

    /*Get Next Index
    Helper method for getting the index of the next waypoint in the path. Use the initial starting waypoint Index and increase it by 1.
    If the next index is equal to the number of child object then we need to reset the path to move the platform back to the beginning.
    */
    public int GetNextIndex(int startIndex){
        // Takes the current waypointIndex and increases it by 1
        int nextIndex = startIndex + 1;
        // If the nextIndex is equal to the number of child objects then set the next index to 0, back to the first index
        if (nextIndex == waypointPath.childCount){
            nextIndex = 0;
        }

        return nextIndex;
    }
    
    /*updateWaypointPath
    As we move between waypoints we want to continue along the path from start to finish. To do so, as we reach our destination waypoint
    along the path we need to update our route to the next waypoint. To do so we set our destination waypoint to be our start and the new destination
    waypoint is the waypoint at the index right after our next waypoint
    */
    private void updateWaypointPath(){
        // Using the previous destinationIndex we will set that waypoint as the new starting waypoint
        startWaypoint = waypointPath.GetChild(destinationIndex);
        // Now we want to update the destinationIndex to match the new destination index by using our helper func 
        destinationIndex = GetNextIndex(destinationIndex);
        // using the newfound destinationIndex we can find the new destinationWaypoint
        destinationWaypoint = waypointPath.GetChild(destinationIndex);

        // Reset the time we took to travel between the two points 
        travelTime = 0;
        // We also need update the time we expect to travel from start to destination. We can do this by finding the distance and dividing it by the speed
        expectedTime = Vector3.Distance(startWaypoint.position,destinationWaypoint.position) / speed;
    }
}