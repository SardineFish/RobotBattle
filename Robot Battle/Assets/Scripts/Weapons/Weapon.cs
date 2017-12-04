using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public abstract class Weapon: NetworkBehaviour
    {
        public float Damage;
        public Vector3 Position;
        public string Name;

        public abstract bool Act();


        public abstract bool Act(Vector3 direction);
    }
}
