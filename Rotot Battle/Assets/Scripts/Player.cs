using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float Speed = 0;
    public float MoveForce = 1;
    public float MaxForce = 10;
    public float Power = 10;
    public float Velocity = 0;
    public float JumpForce = 10;
    public bool OnGround = false;
    new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        var v = rigidbody.velocity.magnitude;
        this.Velocity = v;
        if (v == 0)
            this.MoveForce = this.MaxForce;
        else
        {
            this.MoveForce = Power / v;
            if (this.MoveForce > MaxForce)
                MoveForce = MaxForce;
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            OnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            OnGround = false;
        }
    }
}
