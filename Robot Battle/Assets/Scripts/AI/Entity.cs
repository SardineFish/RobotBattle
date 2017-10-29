using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class Entity : MonoBehaviour
    {
        State state;
        public State State
        {
            get
            {
                return state;
            }
            set
            {
                if (state != null)
                {
                    state.OnExit(value);
                }
                PreviousState = state;
                state = value;
                value.OnEnter(PreviousState);
            }
        }

        public State PreviousState { get; private set; }

        State globalState;
        public State GlobalState
        {
            get { return globalState; }
            set
            {
                if (globalState != null)
                    globalState.OnExit(value);
                var previous = globalState;
                globalState = value;
                value.OnEnter(previous);
            }
        }
        
        public void ChangeState(State state)
        {
            State = state;
        }
        public void ChangeGlobalState(State state)
        {
            this.GlobalState = state;
        }

        public void OnUpdate(UnityEngine.Time time)
        {
            if (GlobalState != null)
                GlobalState.OnUpdate(time);
            if (State != null)
                State.OnUpdate(time);

        }

        public static Entity Create(GameObject gameObject)
        {
            return gameObject.AddComponent<Entity>();
        }
    }
}
