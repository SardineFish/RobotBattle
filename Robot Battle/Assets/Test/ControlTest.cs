using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.AI.Goals;

namespace Assets.Test
{
    public class ControlTest:MonoBehaviour
    {
        public Player Player;
        public GameObject CameraObject;
        public Camera Camera;
        private void Start()
        {
            Player = gameObject.GetComponent<Player>();
            if (CameraObject)
                Camera = CameraObject.GetComponent<Camera>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var ray = Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 5000, 1 << 9))
                {
                    Player.GoalsManager.Goals.Clear();
                    Player.GoalsManager.Goals.Add(new ToDestination(Player, hit.point));
                }
            }
        }
    }
}
