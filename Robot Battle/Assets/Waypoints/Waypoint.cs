using Assets.Waypoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour {
    public List<Waypoint> Connection = new List<Waypoint>();

	// Use this for initialization
	void Start () {
        GetComponent<Collider>().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Collider>().enabled = false;
    }

    public List<Waypoint> SearchPath(Waypoint dst)
    {
        SortedList<float, Edge> visit = new SortedList<float, Edge>();
        Dictionary<Waypoint, Waypoint> FatherWaypoint = new Dictionary<Waypoint, Waypoint>();
        foreach(var next in this.Connection)
        {
            var edge = new Edge(this, next);
            visit.Add(edge.Distance, edge);
        }
        while (true)
        {
            if (visit.Count <= 0)
                return null;
            var minEdge = visit.Values[0];
            var distanceToStart = visit.Keys[0];
            visit.RemoveAt(0);
            var waypoint = minEdge.To;
            if (FatherWaypoint.ContainsKey(waypoint))
                continue;
            FatherWaypoint.Add(minEdge.To, minEdge.From);
            if (waypoint == dst)
                break;
            foreach (var next in waypoint.Connection)
            {
                var edge = new Edge(waypoint, next);
                visit.Add(distanceToStart + edge.Distance, edge);
            }
        }
        List<Waypoint> path = new List<Waypoint>();
        for(Waypoint p = dst; p!= this; p = FatherWaypoint[p])
        {
            path.Insert(0, p);
        }
        path.Insert(0, this);
        return path;
    }
}
