using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class PositionMemory
    {
        Vector3 position;
        public Vector3 Position
        {
            get { return position; }
            set
            {
                position = value;
                LastVisualTime = Time.time;
            }
        }
        public float LastVisualTime { get; set; }
        public Entity Entity { get; set; }
        public PositionMemory (Entity entity, Vector3 pos)
        {
            Position = pos;
            Entity = entity;
        }
    }
}
