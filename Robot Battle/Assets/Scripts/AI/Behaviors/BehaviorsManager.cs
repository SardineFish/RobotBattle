using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Behaviors
{
    public class BehaviorsManager
    {
        public List<Behavior> Behaviors { get; private set; }
        public Dictionary <Type,BehaviorGroup<Behavior>> BehaviorGroups { get; private set; } 

        public void AddBehavior<BehaviorT>(BehaviorT behavior) where BehaviorT: Behavior
        {
            if (!BehaviorGroups.ContainsKey(behavior.GetType()))
            {
                BehaviorGroups.Add(
                    behavior.GetType(),
                    new BehaviorGroup<BehaviorT> ()
                    );

            }
        }
    }
}
