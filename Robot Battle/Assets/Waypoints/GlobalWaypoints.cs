using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace Assets.Waypoints
{

    [ExecuteInEditMode]
    public class GlobalWaypoints: MonoBehaviour
    {
        public List<Waypoint> Waypoints = new List<Waypoint>();
        public bool DrawingMode = false;
        public void AddWaypoint(Waypoint waypoint)
        {
            if (!Waypoints.Contains(waypoint))
                Waypoints.Add(waypoint);
        }
        public void AddWaypoint(Vector3 position)
        {
            var waypoint = GameObject.Instantiate(Resources.Load("Waypoint") as GameObject, GameObject.Find("Waypoints").transform);
            waypoint.name = "Waypoint";
            waypoint.transform.position = position;
            AddWaypoint(waypoint.GetComponent<Waypoint>());
        }
        public void RemoveWaypoint(Waypoint waypoint)
        {
            if (Waypoints.Contains(waypoint))
            {
                Waypoints.Remove(waypoint);
                GameObject.Destroy(waypoint);
            }
        }
    }
}
