using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Global : MonoBehaviour {
    public float Gravity = 9.8f;
    
	// Use this for initialization
	void Start () {
        var members = typeof(Vector3).GetFields();
        
        foreach(var mem in members)
        {
            var attr = mem.GetCustomAttributes(true);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        Physics.gravity = new Vector3(0, -1, 0) * Gravity;
	}
}
