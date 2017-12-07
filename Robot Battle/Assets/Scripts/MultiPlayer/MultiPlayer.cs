using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiPlayer : MonoBehaviour {
    public bool LocalPlayer = false;
	// Use this for initialization
	void Start () {
		
	}

    public override void OnStartLocalPlayer()
    {
        LocalPlayer = isLocalPlayer;
        GameSystem.Now.Controller.GetComponent<ActionController>().ControllingGameObject = gameObject;
        GameSystem.Now.Controller.GetComponent<CameraController>().ControllingGameObject = gameObject;
        GameSystem.Now.Controller.GetComponent<CameraFollow>().FollowTarget = gameObject;
        GameSystem.Now.GameStart();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
