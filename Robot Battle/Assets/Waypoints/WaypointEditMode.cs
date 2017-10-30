using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Waypoints
{
    [ExecuteInEditMode]
    public class WaypointEditMode:MonoBehaviour
    {
        public Waypoint Waypoint;
        
        [ExecuteInEditMode]
        private void Start()
        {
            Waypoint = GetComponent<Waypoint>();
            foreach (var obj in GameObject.FindGameObjectsWithTag("Waypoint"))
            {
                var targetWaypoint = obj.GetComponent<Waypoint>();
            }
        }

        [ExecuteInEditMode]
        private void Update()
        {
            Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.blue);
            foreach (var waypoint in Waypoint.Connection)
            {
                Debug.DrawLine(transform.position, waypoint.transform.position, Color.blue);
            }
        }
    }
}
