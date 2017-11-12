using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Goals
{
    public class ExploreMap : Goal
    {
        public Set<Waypoint> ExploredWaypoints = new Set<Waypoint>();
        public Set<Waypoint> UnexploredWaypoints = new Set<Waypoint>();
        

        public ExploreMap(Player player) : base(player)
        {
            Interval = 0.3f;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            for(var i = 0; i < UnexploredWaypoints.Count; i++)
            {
                if (Player.CanSee(UnexploredWaypoints[i].transform.position))
                {
                    ExploredWaypoints.Add(UnexploredWaypoints[i]);
                    UnexploredWaypoints.RemoveAt(i);
                    i--;
                }
            }
            for (var i = 0; i < ExploredWaypoints.Count; i++)
            {
                var explored = ExploredWaypoints[i];
                foreach (var waypoint in explored.Connection)
                {
                    if (!ExploredWaypoints.Contains(waypoint))
                    {
                        UnexploredWaypoints.Add(waypoint);
                    }
                }
            }

        }

        public override void OnActive()
        {
            base.OnActive();
            if (ExploredWaypoints.Count <= 0)
            {
                foreach (var waypoint in Map.Waypoints)
                {
                    if (Player.CanGoStraight(waypoint.transform.position))
                    {
                        UnexploredWaypoints.Add(waypoint);
                    }
                }
            }
        }
    }
}
