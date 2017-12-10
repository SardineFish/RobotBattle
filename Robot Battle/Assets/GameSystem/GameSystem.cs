using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System;

public class GameSystem : Singleton<GameSystem> {
    public CursorLockMode CursorLockMode = CursorLockMode.Locked;
    public GameObject Controller;
    public bool GameStarted = false;
    public bool ControllerAttached = false;

    public GameObject PlayerObject;

    [SerializeField]
    public Team[] AvailableTeams;

    private static PriorityQueue<float,Action> timeoutQueue = new PriorityQueue<float, Action> (PriorityOrder.Ascending);
    // Use this for initialization
    void Start ()
	{
        Current = this;

	}

    public void GameStart(GameObject playerObj)
    {
        PlayerObject = playerObj;
        GameStarted = true;
        MainGUI.Current.GameGUI.SetActive(true);
    }

    public void PlayerDie()
    {
        MainGUI.Current.DeathScreen.SetActive(true);
        MainGUI.Current.GameGUI.SetActive(false);
        SetTimeOut(() =>
        {
            ClientScene.RemovePlayer(0);
            var teamid = PlayerObject.GetComponent<Player>().TeamID;
            PlayerObject.GetComponent<Player>().CmdDestroy();
            NetworkSystem.Current.JoinTeam(teamid);
            MainGUI.Current.DeathScreen.SetActive(false);
        }, 3);
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
	    for (var i = 0; i < timeoutQueue.Count; i++)
	    {
	        if (timeoutQueue.Keys[i] < Time.time)
	        {
	            timeoutQueue.RemoveAt(i).Invoke();
	            i--;
	        }
	    }
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

    public static void SetTimeOut(Action callback, float time = 0)
    {
        timeoutQueue.Add(Time.time + time, callback);
    }

}
