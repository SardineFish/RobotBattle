using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Messages
{
    public class Message
    {
        public Entity Sender { get; private set; }
        public Entity Receiver { get; private set; }
        public Object Data { get; private set; }
        public bool Handled { get; set; }

        public Message(Entity sender, Entity receiver, object data = null)
        {
            this.Sender = sender;
            this.Receiver = receiver;
            this.Data = data;
            Handled = false;
        }

        public virtual void Dispatch()
        {
            if (!this.Handled && Receiver.State != null)
                Receiver.State.OnMessage(this);
            if (!this.Handled && Receiver.State != null)
                Receiver.GlobalState.OnMessage(this);
            if (!this.Handled)
                Receiver.OnMessage(this);
        }
    }
}
