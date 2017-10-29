using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Behaviors
{
    public class Behavior
    {
        public Player Player { get; private set; }
        public float Interval { get; set; }
        internal float LastUpdate { get; set; }

        public Behavior(Player player)
        {
            this.Player = player;
        }
        public Behavior(Player player, float interval)
        {
            this.Player = player;
            this.Interval = interval;
        }

        public virtual void TryUpdate()
        {
            if (Time.time - LastUpdate < Interval)
                return;
            LastUpdate = Time.time;
            Update();
        }

        public virtual void Update()
        {

        }

        public virtual void Start()
        {

        }
    }
}
