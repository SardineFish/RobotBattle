using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class State
    {
        public Entity Entity { get; set; }
        public bool Disposed { get; private set; }
        public GameObject gameObject
        {
            get { return Entity.gameObject; }
        }

        public virtual void OnEnter(State previousState) { }
        public virtual void OnExit(State nextState)
        {
            this.Disposed = true;
        }
        public virtual void OnUpdate(Time time)
        {

        }
        public State(Entity entity)
        {
            this.Entity = entity;
            this.Disposed = false;
        }

    }
}
