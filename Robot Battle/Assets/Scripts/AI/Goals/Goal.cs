using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.Behaviors;
using UnityEngine;

namespace Assets.Scripts.AI.Goals
{
    public class Goal
    {
        public float Interval;

        public List<Goal> subGoals = new List<Goal>();
        public List<Goal> SubGoals
        {
            get { return subGoals; }
            private set { subGoals = value; }
        }


        public Player Player { get; private set; }

        public delegate void GoalEventHandler(Player player, Goal goal);

        public virtual event GoalEventHandler Achieved;

        public virtual event GoalEventHandler Error;


        public bool Active = false;

        float lastUpdate = 0;

        public Goal(Player player)
        {
            Player = player;
        }

        private void Update()
        {
            foreach (var goal in subGoals)
            {
                goal.TryUpdate();
            }
        }

        public virtual void Update(float dt)
        {

        }

        public virtual bool TryUpdate()
        {
            if (!Active)
            {
                Active = true;
                OnActive();
            }
            if (Time.time - lastUpdate < Interval)
            {
                Update();
                return false;
            }
            var dt = Time.time - lastUpdate;
            lastUpdate = Time.time;
            Update(dt);
            Update();
            return true;
        }

        public virtual void OnActive()
        {

        }

        public virtual void OnEnd()
        {

        }

        public virtual void AddSubGoal(Goal goal)
        {
            SubGoals.Add(goal);
        }
    }
}
