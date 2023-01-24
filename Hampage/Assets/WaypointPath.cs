using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Assigned to the collection of waypoints to help manage which waypoints to navigate to next
public class WaypointPath : MonoBehaviour
{
    // Getting a waypoint with a specific index. Passing in index = 0 will return waypoint1
    public Transform GetWaypoint(int waypointIndex){
        // The waypoints are children of the waypointpath so we can call them using .GetChild
        return transform.GetChild(waypointIndex);
    }
    // Gets the next waypointIndex in the path
    public int GetNextWaypointIndex(int currentWaypointIndex){
        // Takes the current waypointIndex and increases it by 1
        int nextWaypointIndex = currentWaypointIndex + 1;
        // If the nextWaypointIndex is equal to the number of child objects then set the next index to 0 
        if (nextWaypointIndex == transform.childCount){
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }
}
