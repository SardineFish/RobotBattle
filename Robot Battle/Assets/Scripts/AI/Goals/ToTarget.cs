using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Goals
{
    public class ToTarget: Goal
    {
        public Waypoint Target { get; set; }
        public List<Waypoint> Path;
        public ToTarget(Waypoint target):base()
        {
            this.Target = target;
        }
        public override void OnActive()
        {
            
            base.OnActive();
        }
    }
}
