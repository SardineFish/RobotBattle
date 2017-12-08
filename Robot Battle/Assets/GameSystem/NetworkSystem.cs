using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSystem : Singleton<NetworkSystem>
{
    private void Start()
    {
        Current = this;
    }

    public string Host = "localhost";
    public int Port = 7777;
    public NetworkClient Client;
    [SerializeField]
    private bool Connected = false;

    public void SetupHost()
    {
        NetworkServer.Listen(Host, Port);
        Client = ClientScene.ConnectLocalServer();
    }
    public void SetupHost(NetworkMessageDelegate callback)
    {
        NetworkServer.Listen(Host, Port);
        Client = ClientScene.ConnectLocalServer();
        Client.RegisterHandler(MsgType.Connect, callback);
    }

    public void Connect()
    {
        Client = new NetworkClient();
        Client.Connect(Host, Port);
    }
    public void Connect(NetworkMessageDelegate callback)
    {
        Client = new NetworkClient();
        Client.Connect(Host, Port);
        Client.RegisterHandler(MsgType.Connect, callback);
    }

    private void OnConnectedToServer()
    {
        Connected = true;
    }
}