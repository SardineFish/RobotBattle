﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonResumeClick()
    {
        gameObject.SetActive(false);
        MainGUI.Current.GameGUI.SetActive(true);
        GameSystem.Current.AttachControl();
    }

    public void ButtonExitClick()
    {
        NetworkSystem.Current.Stop();
        MainGUI.Current.MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
