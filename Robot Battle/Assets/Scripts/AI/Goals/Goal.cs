using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.Behaviors;

namespace Assets.Scripts.AI.Goals
{
    public class Goal
    {
        public List<Goal> SubGoals = new List<Goal>();
        public List<Behavior> Behaviors = new List<Behavior>();

        public virtual void Update()
        {
            foreach (var goal in SubGoals)
            {
                goal.Update();
            }
            foreach (var behavior in Behaviors)
            {
                behavior.TryUpdate();
            }
        }
    }
}
