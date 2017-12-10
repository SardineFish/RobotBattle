using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.GameSystem
{

    public class MultiplayerSystem: NetworkBehaviour
    {
        public static MultiplayerSystem Current { get; protected set; }
        private void Start()
        {
            Current = this;
        }
        
        [Command]
        public void CmdJoinTeam(int id)
        {
            
        }
    }
}
