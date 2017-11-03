using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Goals
{
    public class GoalsManager: MonoBehaviour
    {
        public List<Goal> Goals = new List<Goal>();
        private void Update()
        {
            foreach (var goal in Goals)
            {
                goal.TryUpdate();
            }
        }

        public void AddGoal(Goal goal)
        {
            if(!Goals.Contains(goal))
            {
                goal.Achieved += Goal_Achieved;
                Goals.Add(goal);
            }
        }

        private void Goal_Achieved(Player player, Goal goal)
        {
            Goals.Remove(goal);
        }
    }
}
