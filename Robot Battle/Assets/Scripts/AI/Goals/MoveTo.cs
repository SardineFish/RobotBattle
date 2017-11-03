using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Goals
{
    public class MoveTo : Goal
    {
        public Vector3 Destination { get; set; }
        public Vector3 StartPosition { get; set; }
        public MoveTo(Player player, Vector3 dst) : base(player)
        {
            Destination = dst;
        }

        public override void OnActive()
        {
            StartPosition = Player.transform.position;
            base.OnActive();
        }

        public override event GoalEventHandler Achieved;

        public override void Update(float dt)
        {
            if((Player.transform.position - Destination ).magnitude < 1)
            {
                if(Achieved != null)
                {
                    Achieved.Invoke(Player, this);
                }
                return;
            }
            Player.LookAt(Destination - Player.transform.position);
            Player.AddMoveBehavior(Destination - Player.transform.position);
            base.Update(dt);
        }

        
    }
}
