using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {
    public CursorLockMode CursorLockMode = CursorLockMode.Locked;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Cursor.lockState = CursorLockMode;
    }
}
