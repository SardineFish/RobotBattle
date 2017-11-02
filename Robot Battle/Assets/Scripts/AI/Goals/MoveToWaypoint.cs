using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Goals
{
    public class MoveToWaypoint : Goal
    {
        public Waypoint Waypoint { get; set; }
        public Vector3 StartPosition { get; set; }
        public MoveToWaypoint(Player player, Waypoint next) : base(player)
        {
            Waypoint = next;
        }

        public override void OnActive()
        {
            StartPosition = Player.transform.position;
            base.OnActive();
        }

        public override event GoalEventHandler Achieved;

        public override void Update(float dt)
        {
            if((Player.transform.position - Waypoint.transform.position ).magnitude < 0.1)
            {
                if(Achieved != null)
                {
                    Achieved.Invoke(Player, this);
                }
                return;
            }
            Player.AddMoveBehavior(Waypoint.transform.position - Player.transform.position);
            base.Update(dt);
        }

        
    }
}
