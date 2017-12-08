using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;

public class GameSystem : Singleton<GameSystem> {
    public CursorLockMode CursorLockMode = CursorLockMode.Locked;
    public GameObject Controller;
    public bool GameStarted = false;
    public bool ControllerAttached = false;
    private NetworkManager networkManager;

    public GameObject PlayerObject;
	// Use this for initialization
	void Start ()
	{
        Current = this;
        networkManager = GetComponent<NetworkManager>();
	}

    public void HostGame(int port)
    {
        networkManager.matchPort = port;
        networkManager.StartHost();
    }

    public void JoinGame(string host,int port)
    {
        networkManager.matchHost = host;
        networkManager.matchPort = port;
        networkManager.StartClient();
    }

    public void GameStart(GameObject playerObj)
    {
        PlayerObject = playerObj;
        GameStarted = true;
    }   

    public void ExitGame()
    {
        
    }

    public void AttachControl()
    {
        Controller.GetComponent<Controller>().SetTarget(PlayerObject);
        ControllerAttached = true;
        Cursor.lockState = CursorLockMode;
    }

    public void ReleaseControl()
    {
        Controller.GetComponent<Controller>().SetTarget(null);
        ControllerAttached = false;
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        ReleaseControl();
	    }
	    if (GameStarted && !ControllerAttached && Input.GetKeyDown(KeyCode.Mouse0))
	    {
	        AttachControl();
	    }
	    if (ControllerAttached)
	        Cursor.lockState = CursorLockMode;
	    else
	        Cursor.lockState = CursorLockMode.None;
	}
}
