using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGUI : Singleton<MainGUI> {
    public GameObject GameGUI;
    public GameObject MainMenu;
    public GameObject ButtonHost;
    public GameObject ButtonJoin;
    public GameObject TextHost;
	// Use this for initialization
	void Start () {
        Current = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonHostClick()
    {

    }

    public void ButtonJoinClick()
    {

    }
}
