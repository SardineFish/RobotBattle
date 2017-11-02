using Assets.Scripts;
using Assets.Waypoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(GlobalWaypoints))]
    public class WayPointEditor: UnityEditor.Editor
    {
        [ExecuteInEditMode]
        private void OnSceneGUI()
        {
            if (Event.current.type == EventType.MouseDown && Event.current.button ==0)
            {
                var drawWaypoint = target as GlobalWaypoints;
                if (drawWaypoint.DrawingMode)
                {
                    var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1000))
                    {
                        Debug.DrawLine(hit.point, hit.point + new Vector3(0, 20, 0), Color.green);
                        drawWaypoint.AddWaypoint(hit.point);
                        /*
                        var waypoint = GameObject.Instantiate(Resources.Load("Waypoint") as GameObject, GameObject.Find("Waypoints").transform);
                        waypoint.name = "Waypoint";
                        waypoint.transform.position = hit.point;
                        */
                    }
                }
            }
        }
    }
}
