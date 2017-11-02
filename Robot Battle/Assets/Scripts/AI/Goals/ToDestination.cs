using Assets.Waypoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Goals
{
    public class ToDestination: Goal
    {
        public Waypoint Destination { get; set; }
        public List<Waypoint> Path;
        public ToDestination(Player player, Waypoint dst) : base(player)
        {
            this.Destination = dst;
        }
        public override void OnActive()
        {
            var globalWaypoint = GameObject.Find("Ground").GetComponent<GlobalWaypoints>();
            var minDst = float.MaxValue;
            Waypoint nearestWaypoint = null;
            foreach (var waypoint in globalWaypoint.Waypoints)
            {
                var distance = (waypoint.transform.position - Player.transform.position).magnitude;
                if (distance < minDst && waypoint.ReachStraight(Player,distance +100))
                {
                    nearestWaypoint = waypoint;
                    minDst = distance;
                }
            }
            this.Path = nearestWaypoint.SearchPath(Destination);
            base.OnActive();
        }
    }
}
