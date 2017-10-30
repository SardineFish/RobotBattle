using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Messages
{
    public class AttackMessage : Message
    {
        public class AttackData
        {
            public float Damage { get; private set; }
            public Vector3 Direction { get; private set; }
            public Entity Attacker { get; private set; }
            public Entity Victim { get; private set; }
            public float ImpactForce { get; private set; }

            public AttackData(Entity attacker, Entity victim) : this(attacker, victim, 0, Vector3.zero, 0) { }
            public AttackData(Entity attacker, Entity victim, float damage) : this(attacker, victim, damage, Vector3.zero, 0) { }
            public AttackData(Entity attacker, Entity victim, float damage, Vector3 direction, float impactForce)
            {
                Damage = damage;
                Direction = direction;
                ImpactForce = impactForce;
                Attacker = attacker;
                Victim = victim;
            }
        }
        public new AttackData Data { get; private set; }
        public AttackMessage(AttackData data) : base(data.Attacker, data.Victim, data)
        {

        }
    }
}
