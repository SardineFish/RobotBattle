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
        public Vector3 Destination { get; set; }
        public List<Waypoint> Path;
        public override event GoalEventHandler Error;
        public override event GoalEventHandler Achieved;
        public ToDestination(Player player, Waypoint dst) : base(player)
        {
            this.Destination = dst.transform.position;
            this.Interval = 0f;
        }
        public ToDestination(Player player, Vector3 position) : base(player)
        {
            this.Interval = 0.1f;
            this.Destination = position;
        }
        public override void OnActive()
        {
            if(this.Destination == null)
            {
                if (Error != null)
                    Error.Invoke(Player, this);
            }
            Waypoint nearestWaypointToDestination = null;
            var minDst = float.MaxValue;
            foreach (var waypoint in GameObject.Find("Map").GetComponent<Map>().Waypoints)
            {
                var distance = (waypoint.transform.position - Destination).magnitude;
                if (distance < minDst)
                {
                    minDst = distance;
                    nearestWaypointToDestination = waypoint;
                }
            }
            var map = GameObject.Find("Map").GetComponent<Map>();
            minDst = float.MaxValue;
            Waypoint nearestWaypointToPlayer = null;
            foreach (var waypoint in map.Waypoints)
            {
                var distance = (waypoint.transform.position - Player.transform.position).magnitude;
                if (distance < minDst && waypoint.ReachStraight(Player,distance +100))
                {
                    nearestWaypointToPlayer = waypoint;
                    minDst = distance;
                }
            }
            this.Path = nearestWaypointToPlayer.SearchPath(nearestWaypointToDestination);
            if (this.Path == null)
            {
                if (Error != null)
                    Error.Invoke(Player, this);
                return;
            }
            var moveGoal = new MoveTo(Player, Path[0].transform.position);
            moveGoal.Achieved += MoveGoal_Achieved;
            this.AddSubGoal(moveGoal);
            CheckStraight();
            base.OnActive();
        }

        public override void Update(float dt)
        {
            if (Path.Count > 0)
                CheckStraight();
            base.Update(dt);
        }

        private void MoveGoal_Achieved(Player player, Goal goal)
        {
            this.SubGoals.Remove(goal);
            if (this.Path.Count > 0)
                Path.RemoveAt(0);
            if (Path.Count <= 0)
            {
                if (Achieved != null)
                    Achieved.Invoke(Player, this);
                return;
            }
            var moveGoal = new MoveTo(Player, Path[0].transform.position);
            moveGoal.Achieved += MoveGoal_Achieved;
            this.AddSubGoal(moveGoal);
        }

        private void CheckStraight()
        {
            if (Player.CanGoStraight(Destination))
            {
                SubGoals.Clear();
                var moveGoal = new MoveTo(Player, Destination);
                moveGoal.Achieved += MoveGoal_Achieved;
                this.AddSubGoal(moveGoal);
                Path.Clear();
                return;
            }
            for (var i = Path.Count - 1; i > 0; i--)
            {
                if (Player.CanGoStraight(Path[i].transform.position))
                {
                    var subPath = new Waypoint[Path.Count - i];
                    Path.CopyTo(i, subPath, 0, Path.Count - i);
                    Path = new List<Waypoint>(subPath);
                    this.SubGoals.Clear();
                    var moveGoal = new MoveTo(Player, Path[0].transform.position);
                    moveGoal.Achieved += MoveGoal_Achieved;
                    this.AddSubGoal(moveGoal);
                }
            }
        }
    }
}
