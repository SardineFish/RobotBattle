using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Goals
{
    public class KeepAway : Goal
    {
        public Player Target { get; set; }
        public float MinDistance { get; set; }
        public float Distance { get; set; }
        public KeepAway(Player player, Player target, float distance) : this(player, target, distance, distance / 2)
        {
        }
        public KeepAway(Player player, Player target, float distance, float minDistance) : base(player)
        {
            Target = target;
            Distance = distance;
            MinDistance = MinDistance;
        }

        public override void Update(float dt)
        {
            var dst = (Target.transform.position - Player.transform.position).magnitude;
            if (dst < Distance)
            {
                var t = (dst - MinDistance) / (Distance - MinDistance);
                var w = -Mathf.Log(t);
                Player.AddMoveBehavior(-(Target.transform.position - Player.transform.position), w);
            }

            base.Update(dt);
        }
    }
}
