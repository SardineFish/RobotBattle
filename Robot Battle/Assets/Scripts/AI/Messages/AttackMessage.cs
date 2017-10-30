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
            public int Damage { get; private set; }
            public Vector3 Direction { get; private set; }
            public Entity Attacker { get; private set; }
            public Entity Victim { get; private set; }

            internal AttackData(Entity attacker, Entity victim, int damage) : this(attacker, victim, damage, Vector3.zero)
            {

            }
            internal AttackData(Entity attacker,Entity victim,int damage, Vector3 direction)
            {
                Damage = damage;
                Direction = direction;
                Attacker = attacker;
                Victim = victim;
            }
        }
        public new AttackData Data { get; private set; }
        public AttackMessage(Entity attacker, Entity victim, int damage):this(attacker,victim,damage, Vector3.zero)
        {

        }
        public AttackMessage(Entity attacker, Entity victim, int damage, Vector3 direction) : base(attacker, victim, new AttackData(attacker, victim, damage, direction))
        {

        }
    }
}
