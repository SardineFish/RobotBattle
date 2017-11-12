using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Behaviors
{
    public class WeightBehavior : Behavior
    {
        public int Weight { get; set; }
        public WeightBehavior(Player player, int weight) : base(player)
        {
            this.Weight = weight;
        }

        public override void Update()
        {
            base.Update();
            
        }
        public virtual void Update(float weight)
        {

        }
    }
}
