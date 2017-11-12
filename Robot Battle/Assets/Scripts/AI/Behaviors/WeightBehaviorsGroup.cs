using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Behaviors
{
    public class WeightBehaviorsGroup<BehaviorT> : BehaviorGroup<BehaviorT> where BehaviorT : WeightBehavior
    {
        public override void Update()
        {
            base.Update();
            int sum = 0;
            foreach (var behavior in this)
            {
                sum += behavior.Weight;
            }
            foreach (var behavior in this)
            {
                behavior.Update((float)(behavior.Weight / sum));
            }
        }
    }
}
