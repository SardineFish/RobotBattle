using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Waypoints
{
    public class Edge
    {
        public Waypoint From { get; set; }
        public Waypoint To { get; set; }
        public float Distance
        {
            get
            {
                return (From.transform.position - To.transform.position).magnitude;
            }
        }
        public Edge(Waypoint from,Waypoint to)
        {
            From = from;
            To = to;
        }
    }
}
