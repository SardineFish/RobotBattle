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

    public void AddConnection(Waypoint waypoint)
    {
        if (waypoint == null)
            return;
        if (!Connection.Contains(waypoint))
            Connection.Add(waypoint);
        if (!waypoint.Connection.Contains(this))
            waypoint.Connection.Add(this);
    }

    public List<Waypoint> SearchPath(Waypoint dst)
    {
        PriorityQueue<float, Edge> visit = new PriorityQueue<float, Edge>();
        Dictionary<Waypoint, Waypoint> FatherWaypoint = new Dictionary<Waypoint, Waypoint>();
        foreach(var next in this.Connection)
        {
            var edge = new Edge(this, next);
            visit.Add(edge.Distance + (next.transform.position - dst.transform.position).magnitude, edge);
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
                visit.Add(distanceToStart + edge.Distance + (next.transform.position - dst.transform.position).magnitude, edge);
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

    public bool ReachStraight(Player player, float maxDistance = 1000)
    {
        var playerHands = player.transform.Find("Wrap/Hands").gameObject;
        var ray = new Ray(transform.position + Vector3.up * 10, playerHands.transform.position - (transform.position + Vector3.up * 10));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform.gameObject == player.gameObject || hit.transform.gameObject== playerHands.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public bool ReachStraight(Waypoint waypoint, float maxDistance = 1000)
    {
        var dst = (transform.position + Vector3.up * 10 - waypoint.transform.position).magnitude;
        dst = Mathf.Min(maxDistance, dst);
        var ray = new Ray(transform.position + Vector3.up * 10, waypoint.transform.position - transform.position + Vector3.up * 10);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dst, 1<<11))
        {
            return false;

            if (hit.transform.gameObject == waypoint.gameObject)
            {
                return true;
            }
        }
        return true;
    }
}
