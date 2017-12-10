using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class AddPlayerMsg : AddPlayerMessage
{
    public  int TeamID;

    public AddPlayerMsg(int teamID):base()
    {
        TeamID = teamID;
    }
    public AddPlayerMsg():base() { }
}