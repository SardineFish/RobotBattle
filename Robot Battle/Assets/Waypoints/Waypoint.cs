using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour {
    public List<Waypoint> Connection = new List<Waypoint>();

	// Use this for initialization
	void Start () {
        GetComponent<Collider>().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Collider>().enabled = false;
    }
}
