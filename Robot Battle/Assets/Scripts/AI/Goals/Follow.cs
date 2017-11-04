using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Goals
{
    public class Follow : Goal
    {
        public Player Target { get; set; }
        public float Distance { get; set; }
        public Follow(Player player, Player target, float distance) : base(player)
        {
            Target = target;
            Interval = 0.5f;
            Distance = distance;
        }

        public override void Update(float dt)
        {
            this.SubGoals.Clear();
            if((Target.transform.position- Player.transform.position).magnitude > Distance)
            {
                this.AddSubGoal(new ToDestination(Player, Target.transform.position));
                this.AddSubGoal(new KeepAway(Player, Target, Distance));
            }
            base.Update(dt);
        }
    }
}
