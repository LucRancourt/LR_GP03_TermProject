
using System;
using UnityEngine;

[System.Serializable]
public class NavPath : MonoBehaviour
{
    // Variables
    private Vector3[] _waypointsPos;


    // Functions
    private void Awake()
    {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();

        _waypointsPos = new Vector3[waypoints.Length];

        for (int i = 0; i < waypoints.Length; i++)
        {
            _waypointsPos[i] = waypoints[i].transform.position;
        }
    }

    public void ReversePath() { Array.Reverse(_waypointsPos); }

    public Vector3[] GetWaypoints() { return _waypointsPos; }

    public int GetWaypointCount() { return _waypointsPos.Length; }
}