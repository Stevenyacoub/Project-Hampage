using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private WaypointPath _waypointPath;

    [SerializeField]
    private float _speed;

    //Index of waypoint the path is moving towards
    private int _targetWaypointIndex;

    // Previous and Target Waypoints
    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    // Time to get to next Waypoint and time elapsed
    private float _timeToWaypoint;
    private float _elapsedTime;

    // Initialize everything
    void Start()
    {
        TargetNextWaypoint();
    }

    // Use Lerp to linearly interpolate between the two waypoints
    void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        elapsedPercentage= Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(_previousWaypoint.position,_targetWaypoint.position, elapsedPercentage);
        //transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation,_targetWaypoint.rotation, elapsedPercentage);

        if(elapsedPercentage >= 1){
            TargetNextWaypoint();
        }
    }

    // Updates fields to target next waypoint in path
    private void TargetNextWaypoint(){
        // setting the new waypoint a previous
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        // setting the next waypoint as the target
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        // Set target waypoint to the new targetWaypointindex
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        // Reset elapsed time
        _elapsedTime = 0;
        // Get distance betweent the waypoints
        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        // Find the timeToWaypoint by dividing the distance by the speed
        _timeToWaypoint = distanceToWaypoint /_speed;
    }
    // If a player is detected on the platform make the player a child of the platform
    private void OnTriggerEnter(Collider other){
        other.transform.SetParent(transform);
    }
    // After player leaves platform remove player as a child 
    private void OnTriggerExit(Collider other) {
        other.transform.SetParent(null);
    }
}