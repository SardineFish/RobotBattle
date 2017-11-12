using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Behaviors
{
    public class PriorityBehaviorsGroup<BehaviorT>: BehaviorGroup<BehaviorT> where BehaviorT: PriorityBehavior
    {
        public override void Update()
        {
            base.Update();
            var maxPriority = int.MinValue;
            foreach (var behavior in this)
            {
                if (behavior.Priority > maxPriority)
                    maxPriority = behavior.Priority;
            }
            foreach (var behavior in this)
            {
                if (behavior.Priority == maxPriority)
                    behavior.Update();
            }
        }
    }
}
