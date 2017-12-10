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
    private NetworkSystem networkSystem;

    public GameObject PlayerObject;

    [SerializeField]
    public Team[] AvailableTeams;
    // Use this for initialization
    void Start ()
	{
        Current = this;
	    networkSystem = NetworkSystem.Current;

	}

    public void GameStart(GameObject playerObj)
    {
        PlayerObject = playerObj;
        GameStarted = true;
        MainGUI.Current.GameGUI.SetActive(true);
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
	    if (GameStarted && Input.GetKeyDown(KeyCode.Escape))
	    {
	        ReleaseControl();
	        MainGUI.Current.GameGUI.SetActive(false);
	        MainGUI.Current.PauseMenu.SetActive(true);
	    }
	    if (ControllerAttached)
	        Cursor.lockState = CursorLockMode;
	    else
	        Cursor.lockState = CursorLockMode.None;
	}

}
