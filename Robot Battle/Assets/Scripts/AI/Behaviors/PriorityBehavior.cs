using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Behaviors
{
    public class PriorityBehavior : Behavior
    {
        int priority;
        public int Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                if (PriorityChanged != null)
                    PriorityChanged.Invoke(this);
            }
        }
        public event Action<Behavior> PriorityChanged;
        public PriorityBehavior(Player player, int priority) : base(player)
        {
            Priority = priority;
        }
    }
}
