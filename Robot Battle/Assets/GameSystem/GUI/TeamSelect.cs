using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonRedClick()
    {
        NetworkSystem.Current.JoinTeam(1, () =>
        {
            gameObject.SetActive(false);
        });
    }

    public void ButtonBlueClick()
    {
        NetworkSystem.Current.JoinTeam(2, () =>
        {
            gameObject.SetActive(false);
        });
    }
}
