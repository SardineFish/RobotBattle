using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Behaviors
{
    public class BehaviorGroup<BehaviorT>: List<BehaviorT> where BehaviorT : Behavior
    {
        public virtual void Update()
        {
        }
    }
}
