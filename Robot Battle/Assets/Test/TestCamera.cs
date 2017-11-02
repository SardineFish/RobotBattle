using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Test
{
    public class TestCamera: MonoBehaviour
    {
        public float ScrollMove = 10;
        public float RotateSens = 3;
        
        private void Update()
        {
            if(Input.mouseScrollDelta.y > 0)
            {
                transform.Translate(Vector3.forward * ScrollMove);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                transform.Translate(-Vector3.forward * ScrollMove);
            }
            
        }
        private void Start()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.zero - transform.position); 
        }
    }
}
