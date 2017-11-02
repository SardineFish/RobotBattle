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

        public override void Update(float dt)
        {
            
            base.Update(dt);
        }

        
    }
}
