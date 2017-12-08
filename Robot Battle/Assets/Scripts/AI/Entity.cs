using Assets.Scripts.AI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.AI
{
    public class Entity : NetworkBehaviour
    {
        public State State
        {
            get
            {
                return GetComponent<State>();
            }
        }

        public State PreviousState { get; private set; }

        State globalState;
        public State GlobalState
        {
            get { return globalState; }
        }
        
        public void ChangeState(Type stateType)
        {
            if (isServer)
            {
                if (stateType == typeof(PlayerDeadState))
                    RpcStateChange(PlayerDeadState.StateName);
            }
            if (!stateType.IsSubclassOf(typeof(State)))
            {
                throw new Exception("A State required.");
            }
            var state = GetComponent<State>();
            Type previouseType = null;
            if (state != null)
            {
                state.OnExit(stateType);
                previouseType = state.GetType();
            }
            GameObject.Destroy(state);
            var nextState = gameObject.AddComponent(stateType) as State;
            if (previouseType != null)
                nextState.OnEnter(previouseType);
        }

        [ClientRpc]
        public void RpcStateChange(string state)
        {
            if (isServer)
                return;
            if (state == PlayerDeadState.StateName)
            {
                this.ChangeState(typeof(PlayerDeadState));
            }
        }

        [Command]
        public void CmdBroadcastStateChange(string state)
        {
            RpcStateChange(state);
        }

        public virtual void OnMessage(Message message)
        {

        }

        public static Entity Create(GameObject gameObject)
        {
            return gameObject.AddComponent<Entity>();
        }
    }
}
