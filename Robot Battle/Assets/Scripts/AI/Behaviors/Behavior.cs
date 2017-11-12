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

        public Behavior(Player player)
        {
            this.Player = player;
        }

        public virtual void Update()
        {

        }

        public virtual void onActive()
        {

        }
    }
}
