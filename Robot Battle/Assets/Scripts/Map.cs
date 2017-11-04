using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class Map : MonoBehaviour
    {
        public List<Waypoint> Waypoints = new List<Waypoint>();
        public List<Waypoint> SubWaypoints = new List<Waypoint>();
        public float ExtendDistance = 10;
        public int MaxExtend = 100;
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
                if (Application.isEditor)
                    GameObject.DestroyImmediate(waypoint.gameObject);
            }
        }
        public void ClearWaypoints()
        {
            ClearSubWaypoints();
            foreach (var waypoint in Waypoints)
            {
                if (Application.isEditor)
                    GameObject.DestroyImmediate(waypoint.gameObject);
            }
            Waypoints.Clear();
        }
        public void ClearSubWaypoints()
        {
            foreach (var subWaypoint in SubWaypoints)
            {
                if (Application.isEditor)
                    DestroyImmediate(subWaypoint.gameObject);
                Waypoints.Remove(subWaypoint);
            }
            SubWaypoints.Clear();
        }
        public void ExtandWaypoints(int max)
        {
            ClearSubWaypoints();
            if (Waypoints.Count <= 0)
                return;
            SubWaypoints.Add(Waypoints[0]);
            for(var i = 0; i < SubWaypoints.Count && i < max; i++)
            {
                var baseWaypoint = SubWaypoints[i];
                for(int j = 0; j < 6; j++)
                {
                    var pos = baseWaypoint.transform.position + (ExtendDistance * new Vector3(Mathf.Cos((j * Mathf.PI / 3)), 0, Mathf.Sin((j * Mathf.PI / 3))));
                    var ray = new Ray(pos + Vector3.up * 500, -Vector3.up * 1000);
                    RaycastHit hit;
                    if(Physics.Raycast(ray,out hit, 1000, 1 << 11))
                    {
                        continue;
                    }
                    if (Physics.Raycast(ray, out hit, 1000, 1 << 10))
                    {
                        
                        if (baseWaypoint.ReachStraight(hit.transform.gameObject.GetComponent<Waypoint>()))
                            baseWaypoint.Connection.Add(hit.transform.gameObject.GetComponent<Waypoint>());
                        continue;
                    }
                    if(Physics.Raycast(ray,out hit, 1000, 1 << 9))
                    {
                        var subWaypoint = GameObject.Instantiate(Resources.Load("SubWaypoint") as GameObject, GameObject.Find("SubWaypoints").transform);
                        subWaypoint.transform.position = pos;
                        if (baseWaypoint.ReachStraight(subWaypoint.GetComponent<Waypoint>()))
                            subWaypoint.GetComponent<Waypoint>().AddConnection(baseWaypoint);
                        SubWaypoints.Add(subWaypoint.GetComponent<Waypoint>());
                    }
                }
            }
            SubWaypoints.RemoveAt(0);
            Waypoints.AddRange(SubWaypoints);
        }
    }
}
