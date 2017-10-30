using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.AI.Messages;

namespace Assets.Scripts.AI
{
    public class State: MonoBehaviour
    {
        public bool Disposed { get; private set; }

        public virtual void OnEnter(Type previousState) { }
        public virtual void OnExit(Type nextState)
        {
            this.Disposed = true;
        }

        public virtual void OnMessage(Message message)
        {
        }

    }
}
