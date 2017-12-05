using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Waypoints
{
    [ExecuteInEditMode]
    public class WaypointEditMode:MonoBehaviour
    {
        public Waypoint Waypoint;
        public float MaxConnectDistance = 500;
        
        [ExecuteInEditMode]
        private void Start()
        {
            if (!Application.isEditor)
                return;
            Waypoint = GetComponent<Waypoint>();
            foreach (var obj in GameObject.FindGameObjectsWithTag("Waypoint"))
            {
                if (obj == gameObject)
                    continue;
                obj.GetComponent<Collider>().enabled = true;
                var origin = transform.position + Vector3.up * 10;
                var ray = new Ray(origin, obj.transform.position - origin);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, MaxConnectDistance))
                {
                    if(hit .transform.gameObject == obj)
                    {
                        var targetWaypoint = obj.GetComponent<Waypoint>();
                        if (!targetWaypoint.Connection.Contains(this.Waypoint))
                        {
                            targetWaypoint.Connection.Add(this.Waypoint);
                            this.Waypoint.Connection.Add(targetWaypoint);
                        }
                    }
                }
            }
        }

        [ExecuteInEditMode]
        private void Update()
        {
            Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.yellow);
            foreach (var waypoint in Waypoint.Connection)
            {
                Debug.DrawLine(transform.position, waypoint.transform.position, Color.blue);
            }
        }
    }
}
