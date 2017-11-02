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
        Queue<Goal> subGoals = new Queue<Goal>();
        public Goal[] SubGoals
        {
            get { return subGoals.ToArray(); }
        }
        public Goal ActiveSubGoal
        {
            get { return subGoals.Peek(); }
        }

        public List<Behavior> Behaviors = new List<Behavior>();

        public List<Goal> ParallelGoals = new List<Goal>();

        public delegate void GoalAchieveEventHandler(Player player, Goal goal);

        public event GoalAchieveEventHandler Achieved;


        public bool Active = false;

        float lastUpdate = 0;

        private void Update()
        {
            if (!Active)
            {
                Active = true;
                OnActive();
            }
            subGoals.Peek().TryUpdate();
            foreach (var behavior in Behaviors)
            {
                behavior.TryUpdate();
            }
        }

        public virtual void Update(float dt)
        {

        }

        public virtual bool TryUpdate()
        {
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
            goal.Achieved += SubGoal_Achieved;
        }

        private void SubGoal_Achieved(Player player, Goal goal)
        {
            var subGoal = subGoals.Dequeue();
            subGoal.Active = false;
            subGoal.OnEnd();
        }
    }
}
