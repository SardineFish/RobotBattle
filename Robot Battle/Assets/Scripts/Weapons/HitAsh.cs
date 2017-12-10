using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class HitAsh : MonoBehaviour
{
    public float LifeTime = 1f;
    void Start()
    {
        GameObject.Destroy(gameObject, LifeTime);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
