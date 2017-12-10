using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public enum NetworkType
{
    Server,
    Client,
    Host,
}
public class NetworkSystem : NetworkManager
{
    private void Start()
    {
        Current = this;
        autoCreatePlayer = false;
    }
    public static NetworkSystem Current { get; protected set; }

    public NetworkClient Client
    {
        get { return client; }
        set { client = value; }
    }

    public NetworkType NetworkType;
    public NetworkServer Server{ get; set; }
    [SerializeField]
    private bool Connected = false;

    private Action hostCallback;
    private Action joinCallback;
    private Action addPlayerCallback;

    public GameObject PlayerRed;
    public GameObject PlayerBlue;

    public int port;

    public void SetupHost(int port)
    {
        /*
        NetworkServer.Listen(port);
        Client = ClientScene.ConnectLocalServer();
        Client.RegisterHandler(MsgType.Connect, (msg) =>
        {
            hostCallback?.Invoke();
        });*/
        networkPort = port;
        Client = StartHost();
        NetworkServer.RegisterHandler(MsgType.AddPlayer, OnAddPlayer);
        /*ClientScene.Ready(Client.connection);
        ClientScene.AddPlayer(0);*/
    }
    public void SetupHost(int port,Action callback)
    {
        hostCallback = callback;
        SetupHost(port);
        
    }

    public void Connect(string address,int port)
    {
        networkAddress = address;
        networkPort = port;
        Client = StartClient();
        Client.RegisterHandler(MsgType.Connect, (msg) =>
        {
            NetworkType = NetworkType.Client;
            joinCallback?.Invoke();
            ClientScene.Ready(Client.connection);
            //ClientScene.AddPlayer(0);
            //ClientScene.AddPlayer(msg.conn, 0, new AddPlayerMsg(1));
        });
        /*Client = new NetworkClient();
        Client.Connect(address, port);
        Client.RegisterHandler(MsgType.Connect, (msg) =>
        {
            joinCallback?.Invoke();
        });*/
    }

    public void Connect(string address, int port, Action callback)
    {
        Debug.Log("connect");
        joinCallback = callback;
        Connect(address, port);
    }

    public void Stop()
    {
        if (NetworkType == NetworkType.Client)
            StopClient();
        switch (NetworkType)
        {
            case NetworkType.Host:
                StopHost();
                break;
            case NetworkType.Client:
                StopClient();
                break;
        }
    }

    public void JoinTeam(int teamID)
    {
        var AddMsg = new AddPlayerMsg(teamID);
        AddMsg.playerControllerId = (short)ClientScene.localPlayers.Count;
        Client.Send(MsgType.AddPlayer, AddMsg);
        Client.RegisterHandler(MsgType.AddPlayer, (msg) =>
        {
            addPlayerCallback?.Invoke();
            addPlayerCallback = null;
        });
    }

    public void JoinTeam(int teamID, Action callback)
    {
        addPlayerCallback = callback;
        JoinTeam(teamID);
    }
    protected void OnAddPlayer(NetworkMessage msg)
    {
        //AddPlayerMessage addMsg;
        //msg.ReadMessage()
        var addMsg = msg.ReadMessage<AddPlayerMsg>();
        var team = GameSystem.Current.AvailableTeams.Where(t => t.TeamID == addMsg.TeamID).FirstOrDefault();
        //var team = GameSystem.Current.AvailableTeams[1];
        if (team == null)
        {
            var errorMsg = new ErrorMessage();
            errorMsg.errorCode = new NullReferenceException().HResult;
            msg.conn.Send(MsgType.Error, errorMsg);
            return;
        }
        //ClientScene.Ready(msg.conn);
        var player = Instantiate(team.PlayerPrefab, team.SpawnPosition, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(msg.conn, player, addMsg.playerControllerId);
        msg.conn.Send(MsgType.AddPlayer, new AddPlayerMessage());
        
    }
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var team = GameSystem.Current.AvailableTeams[1];
        var player = Instantiate(team.PlayerPrefab, team.SpawnPosition, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        //base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
        var team = GameSystem.Current.AvailableTeams[0];
        var player = Instantiate(team.PlayerPrefab, team.SpawnPosition, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
    // Callback when start host
    public override void OnStartHost()
    {
        NetworkType = NetworkType.Host;
        Debug.Log("host");
        base.OnStartHost();
        hostCallback?.Invoke();
        hostCallback = null;
    }

    // Callback both start host & client
    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("start client");
        base.OnStartClient(client);
        Client = client;

        joinCallback?.Invoke();
        joinCallback = null;
    }
    
}