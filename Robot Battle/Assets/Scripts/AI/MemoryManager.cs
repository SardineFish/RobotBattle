using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class PositionMemoryManager
    {
        public Dictionary<Entity, PositionMemory> PositionMemories { get; private set; }
        public PositionMemory this[Entity entity]
        {
            get
            {
                if (PositionMemories.ContainsKey(entity))
                    return PositionMemories[entity];
                return null;
            }
            private set
            {
                PositionMemories[entity] = value;
            }
        }
        public PositionMemoryManager()
        {
            PositionMemories = new Dictionary<Entity, PositionMemory>();
        }

        public void UpdateMemory(Entity entity, Vector3 position)
        {
            if (PositionMemories.ContainsKey(entity))
                PositionMemories[entity].Position = position;
            else
                PositionMemories[entity] = new PositionMemory(entity, position);
        }
    }
}
