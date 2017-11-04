using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Goals
{
    public class DetectEnemy : Goal
    {
        public override event GoalEventHandler Achieved;
        public DetectEnemy(Player player) : base(player)
        {
            this.Interval = 0.1f;
        }

        public override void Update(float dt)
        {
            Player.VisualScan();
            foreach(var memory in Player.Memory.PositionMemories)
            {
                if(memory.Value.Entity is Player)
                {
                    if (Achieved != null)
                        Achieved(Player, this);
                    return;
                }
            }
            base.Update(dt);
        }
    }
}
