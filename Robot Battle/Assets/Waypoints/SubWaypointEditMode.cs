using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Waypoints
{
    [ExecuteInEditMode]
    public class SubWaypointEditMode:MonoBehaviour
    {
        public Waypoint Waypoint;
        public float MaxConnectDistance = 500;
        
        [ExecuteInEditMode]
        private void Start()
        {
            Waypoint = GetComponent<Waypoint>();
        }

        [ExecuteInEditMode]
        private void Update()
        {
            foreach (var waypoint in Waypoint.Connection)
            {
                Debug.DrawLine(transform.position, waypoint.transform.position, Color.blue);
            }
        }
    }
}
