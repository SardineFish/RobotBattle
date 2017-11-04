using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Goals
{
    public class Attack : Goal
    {
        public Player Target { get; set; }
        public Attack(Player player, Player target) : base(player)
        {
            Target = target;
            Interval = 0.1f;
        }

        public override void Update(float dt)
        {
            var self = Player.transform.Find("Wrap/Hands");
            var target = Target.transform.Find("Wrap/Hands");
            Player.LookAt(target.position - self.position);
            base.Update(dt);
        }

        public override bool TryUpdate()
        {
            Player.Fire();
            return base.TryUpdate();
        }
    }
}
