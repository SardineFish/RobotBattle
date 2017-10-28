using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float BulletSpeed = 100;
    public float BulletLifeTime = 1;
	
	// Use this for initialization
	void Start () {
        GameObject.Destroy(gameObject, BulletLifeTime);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
    }
}
