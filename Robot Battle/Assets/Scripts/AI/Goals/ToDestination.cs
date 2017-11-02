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
        public override event GoalEventHandler Error;
        public override event GoalEventHandler Achieved;
        public ToDestination(Player player, Waypoint dst) : base(player)
        {
            this.Destination = dst;
        }
        public ToDestination(Player player, Vector3 position) : base(player)
        {
            var minDst = float.MaxValue;
            Waypoint nearestWaypoint = null;
            foreach (var waypoint in GameObject.Find("Map").GetComponent<Map>().Waypoints)
            {
                var distance = (waypoint.transform.position - player.transform.position).magnitude;
                if (distance < minDst)
                {
                    minDst = distance;
                    nearestWaypoint = waypoint;
                }
            }
            this.Destination = nearestWaypoint;
        }
        public override void OnActive()
        {
            if(this.Destination == null)
            {
                if (Error != null)
                    Error.Invoke(Player, this);
            }
            var map = GameObject.Find("Map").GetComponent<Map>();
            var minDst = float.MaxValue;
            Waypoint nearestWaypoint = null;
            foreach (var waypoint in map.Waypoints)
            {
                var distance = (waypoint.transform.position - Player.transform.position).magnitude;
                if (distance < minDst && waypoint.ReachStraight(Player,distance +100))
                {
                    nearestWaypoint = waypoint;
                    minDst = distance;
                }
            }
            this.Path = nearestWaypoint.SearchPath(Destination);
            if (this.Path == null)
            {
                if (Error != null)
                    Error.Invoke(Player, this);
                return;
            }
            var moveGoal = new MoveToWaypoint(Player, Path[0]);
            moveGoal.Achieved += MoveGoal_Achieved;
            this.AddSubGoal(moveGoal);
            base.OnActive();
        }

        private void MoveGoal_Achieved(Player player, Goal goal)
        {
            this.SubGoals.Remove(goal);
            Path.RemoveAt(0);
            if (Path.Count <= 0)
            {
                if (Achieved != null)
                    Achieved.Invoke(Player, this);
                return;
            }
            var moveGoal = new MoveToWaypoint(Player, Path[0]);
            moveGoal.Achieved += MoveGoal_Achieved;
            this.AddSubGoal(moveGoal);
        }
    }
}
