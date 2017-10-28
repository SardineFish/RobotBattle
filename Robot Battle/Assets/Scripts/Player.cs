using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float Speed = 0;
    public float MoveForce = 1;
    public float MaxForce = 10;
    public float Power = 10;
    public Vector3 Velocity ;
    public float JumpForce = 10;
    public float ForceFly = 5;
    public float PowerFly = 1;
    public bool OnGround = false;
    public int MaxJump = 2;
    public int JumpCount = 0;
    public float ShootDuration = 1;
    new Rigidbody rigidbody;
    BoxCollider footCollider;
    CapsuleCollider bodyCollider;
    float lastShootTime = 0;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        footCollider = GetComponent<BoxCollider>();
        bodyCollider = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        var v = (Vector3.Scale(rigidbody.velocity, new Vector3(1, 0, 1)));
        this.Velocity = rigidbody.velocity;
        /*if (this.Velocity.y < 0.01)
        {
            this.OnGround = true;
            this.JumpCount = 0;
        }*/
        
        if (this.OnGround)
        {
            if (v.magnitude == 0)
                this.MoveForce = this.MaxForce;
            else
            {
                this.MoveForce = Power / v.magnitude;
                if (this.MoveForce > MaxForce)
                    MoveForce = MaxForce;
            }
        }
        else
        {
            if (v.magnitude <= 0)
                this.MoveForce = this.ForceFly;
            else
            {
                this.MoveForce = PowerFly / v.magnitude;
                if (this.MoveForce > this.ForceFly)
                    this.MoveForce = this.ForceFly;
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        OnGround = true;
        JumpCount = 0;
    }
    private void OnTriggerExit(Collider other)
    {
        OnGround = false;
    }

    public void Jump()
    {
        if (JumpCount < MaxJump)
        {
            rigidbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
            JumpCount++;
        }
    }

    public void Shoot()
    {
        if (Time.time - lastShootTime < ShootDuration)
            return;
        lastShootTime = Time.time;
        var gunLeft = transform.Find("Wrap/Hands/Gun-L/Gun-Inside/Gun-Barrel").gameObject;
        var gunRight = transform.Find("Wrap/Hands/Gun-R/Gun-Inside/Gun-Barrel").gameObject;
        var rayL = new Ray(gunLeft.transform.position, -gunLeft.transform.right);
        var rayR = new Ray(gunRight.transform.position, -gunRight.transform.right);
        Debug.DrawLine(rayL.origin, rayL.origin + rayL.direction * 100, Color.red);
        Debug.DrawLine(rayR.origin, rayR.origin + rayR.direction * 100, Color.red);
        var bulletL = Instantiate(Resources.Load("Bullet") as GameObject);
        var bulletR = Instantiate(Resources.Load("Bullet") as GameObject);
        bulletL.transform.position = gunLeft.transform.position;
        bulletR.transform.position = gunRight.transform.position;
        bulletL.transform.rotation = Quaternion.LookRotation(rayL.direction);
        bulletR.transform.rotation = Quaternion.LookRotation(rayR.direction);
        var x = bulletL.transform.forward;
    }
}
