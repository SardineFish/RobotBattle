using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class PlayerDeadState: State
    {
        public const string StateName = "PlayerDeadState";
        public float DestoryTime = 5;
        void Start()
        {
            transform.Find("Wrap/Body").gameObject.AddComponent<Rigidbody>().useGravity = true;
            transform.Find("Wrap/Body").gameObject.GetComponent<Collider>().enabled = true;
            transform.Find("Wrap/Body/Shield-L").gameObject.AddComponent<Rigidbody>().useGravity = true;
            transform.Find("Wrap/Body/Shield-L").gameObject.GetComponent<Collider>().enabled = true;
            transform.Find("Wrap/Body/Shield-R").gameObject.AddComponent<Rigidbody>().useGravity = true;
            transform.Find("Wrap/Body/Shield-R").gameObject.GetComponent<Collider>().enabled = true;
            transform.Find("Wrap/Hands/Gun-L").gameObject.AddComponent<Rigidbody>().useGravity = true;
            transform.Find("Wrap/Hands/Gun-L").gameObject.GetComponent<Collider>().enabled = true;
            transform.Find("Wrap/Hands/Gun-R").gameObject.AddComponent<Rigidbody>().useGravity = true;
            transform.Find("Wrap/Hands/Gun-R").gameObject.GetComponent<Collider>().enabled = true;
            transform.Find("Wrap/Hands").gameObject.GetComponent<Collider>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            if (isLocalPlayer)
                GameSystem.Current.PlayerDie();
        }
    }
}
