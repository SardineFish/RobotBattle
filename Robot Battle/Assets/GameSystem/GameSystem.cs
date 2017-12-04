using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameSystem : MonoBehaviour {
    public CursorLockMode CursorLockMode = CursorLockMode.Locked;
    public GameObject Controller;
    public bool GameStarted = false;
	// Use this for initialization
	void Start () {
        Now = this;
	}

    public static GameSystem Now;

    public void GameStart()
    {
        GameStarted = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameStarted)
            Cursor.lockState = CursorLockMode;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
