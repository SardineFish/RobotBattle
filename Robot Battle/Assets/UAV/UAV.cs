using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAV : MonoBehaviour {
    public List<GameObject> Pivots = new List<GameObject>();
    public Vector3 Acceleration;
    public new Rigidbody rigidbody { get; set; }
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        
    }
}
