using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Assets.Scripts.AI.StateMachine;
using Assets.Scripts.AI;

public class MultiPlayer : NetworkBehaviour {
    public bool LocalPlayer = false;
	// Use this for initialization
	void Start () {
		
	}

    public override void OnStartLocalPlayer()
    {
        LocalPlayer = isLocalPlayer;
        GameSystem.Current.GameStart(gameObject);
        GameSystem.Current.AttachControl();
    }

    // Update is called once per frame
    void Update ()
    {
        if (isLocalPlayer && GetComponent<Player>().State is PlayerDeadState)
        {
            GameSystem.Current.Controller.GetComponent<Controller>().SetTarget(null);
            GameSystem.Current.ReleaseControl();
        }
	}
}
