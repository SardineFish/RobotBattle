using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;
using Assets.Scripts.Weapons;
using Assets.Scripts.AI.Messages;
using Assets.Scripts.AI.Goals;

public class Player : Assets.Scripts.AI.Entity
{
    public float Speed = 0;
    public float MoveForce = 1;
    public float MaxForce = 10;
    public float Power = 10;
    public Vector3 Velocity;
    public float JumpForce = 10;
    public float ForceFly = 5;
    public float PowerFly = 1;
    public bool OnGround = false;
    public int MaxJump = 2;
    public int JumpCount = 0;
    public float VisualDistance = 500;
    public float VisualRange = 90;
    public float ShootImpact = 1;
    public float MaxTurnSpeed = 180;
    public float BodyRadius = 5;
    public Ray Looking;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    public Vector3 MoveDirection
    {
        get
        {
            return moveDirection;
        }
        set
        {
            moveDirection = value.normalized;
        }
    }

    public float HP = 100;
    public float Defence = 0;

    public List<PositionMemory> PositionMemories = new List<PositionMemory>();
    public GoalsManager GoalsManager;
    new Rigidbody rigidbody;
    BoxCollider footCollider;
    CapsuleCollider bodyCollider;

    float lastShootTime = 0;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        footCollider = GetComponent<BoxCollider>();
        bodyCollider = GetComponent<CapsuleCollider>();
        GoalsManager = GetComponent<GoalsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(MoveDirection);
        MoveDirection = Vector3.zero;
        var v = (Vector3.Scale(rigidbody.velocity, new Vector3(1, 0, 1)));
        this.Velocity = rigidbody.velocity;

        this.Looking = new Ray(transform.Find("Wrap/Hands").position, -transform.Find("Wrap/Hands").right);

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

    private void Move(Vector3 direction)
    {
        direction = Vector3.Scale(direction, new Vector3(1, 0, 1));
        rigidbody.AddForce(direction * MoveForce, ForceMode.Impulse);
    }

    public void AddMoveBehavior(Vector3 direction, float weight = 1)
    {
        MoveDirection = MoveDirection + direction.normalized * weight;
    }

    public bool MoveTo(Vector3 target)
    {
        var dx = Vector3.Scale(target - transform.position, new Vector3(1, 0, 1));
        if (dx.magnitude < 0.01)
            return true;
        else
        {
            Move(dx);
            return false;
        }
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
        var gun = GetComponent<Gun>();
        if (gun != null)
            gun.Shoot();
    }

    public void LookAt(Vector3 forward)
    {
        var hands = transform.Find("Wrap/Hands");
        var rotation = Quaternion.LookRotation(forward);
        var hor = LimitAngle(rotation.eulerAngles.y - this.transform.rotation.eulerAngles.y);
        var ver = LimitAngle(-(rotation.eulerAngles.x + hands.localEulerAngles.y));
        if (Mathf.Abs(hor) > MaxTurnSpeed * Time.deltaTime)
            hor = Mathf.Sign(hor) * MaxTurnSpeed * Time.deltaTime;
        if (Mathf.Abs(ver) > MaxTurnSpeed * Time.deltaTime)
            ver = Mathf.Sign(ver) * MaxTurnSpeed * Time.deltaTime;
        transform.Rotate(0, hor, 0, Space.Self);
        hands.Rotate(0, ver, 0, Space.Self);
        var ray = new Ray(hands.position, -hands.right);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * VisualDistance, Color.red);
        RaycastHit hit;
        var gunL = transform.Find("Wrap/Hands/Gun-L");
        var gunR = transform.Find("Wrap/Hands/Gun-R");
        gunL.localRotation = new Quaternion();
        gunR.localRotation = new Quaternion();
        if (Physics.Raycast(ray, out hit, VisualDistance))
        {
            Debug.DrawLine(hit.point, hit.point + new Vector3(20, 0, 0), Color.red);
            Debug.DrawLine(hit.point, hit.point + new Vector3(0, 20, 0), Color.green);
            Debug.DrawLine(hit.point, hit.point + new Vector3(0, 0, 20), Color.blue);

            var point = hit.point;
            var gunLForward = -gunL.right;
            var gunRForward = -gunR.right;
            var turnL = Quaternion.Angle(Quaternion.LookRotation(gunLForward), Quaternion.LookRotation(point - gunL.position));
            var turnR = Quaternion.Angle(Quaternion.LookRotation(gunRForward), Quaternion.LookRotation(point - gunR.position));
            //var turnR = Quaternion.FromToRotation(gunRForward, point - gunR.position);
            gunL.Rotate(0, 0, turnL);
            gunR.Rotate(0, 0, -turnR);
        }
    }

    public bool CanGoStraight(Vector3 position)
    {
        var distance = (position - transform.position).magnitude;
        var ang = Mathf.Acos(BodyRadius / distance) / Mathf.PI * 180;
        var ray = new Ray(transform.Find("Wrap/Hands").position, position - transform.position);
        var rayL = ray;
        var rayR = ray;
        rayL.origin += (Quaternion.AngleAxis(ang, Vector3.up) * rayL.direction) * BodyRadius;
        rayR.origin += (Quaternion.AngleAxis(-ang, Vector3.up) * rayR.direction) * BodyRadius;
        /*
        rayL.direction = Quaternion.AngleAxis(ang, Vector3.up) * rayL.direction;
        rayR.direction = Quaternion.AngleAxis(-ang, Vector3.up) * rayR.direction;
        */
        Debug.DrawLine(rayL.origin, rayL.origin + rayL.direction * distance, Color.cyan);
        Debug.DrawLine(rayR.origin, rayR.origin + rayR.direction * distance, Color.cyan);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.cyan);
        if (!Physics.Raycast(rayL, distance, 1 << 11) && !Physics.Raycast(rayR, distance, 1 << 11) && !Physics.Raycast(ray, distance, 1 << 11))
        {
            return true;
        }
        return false;
    }

    /*public void OnShotCallback(Player shooter, Vector3 direction, float Damage, float impactForce)
    {
        this.HP -= Damage;
        rigidbody.AddForce(direction.normalized * impactForce, ForceMode.Impulse);
        if (this.HP < 0)
        {
            ChangeState(typeof(PlayerDeadState));
        }
    }*/

    public override void OnMessage(Message message)
    {
        if (message is AttackMessage)
        {
            var attack = message.Data as AttackMessage.AttackData;
            if (attack != null)
            {
                this.HP -= attack.Damage;
                rigidbody.AddForce(attack.Direction.normalized * attack.ImpactForce, ForceMode.Impulse);
                if (this.HP < 0)
                {
                    ChangeState(typeof(PlayerDeadState));
                }
            }
            message.Handled = true;
        }
        else
            base.OnMessage(message);
    }

    public void VisualScan()
    {
        foreach(var obj in GameObject.FindGameObjectsWithTag("Player"))
        {

        }
    }


    float LimitAngle(float angle)
    {
        angle -= ((int)(angle / 360)) * 360;
        if (angle > 180)
            return angle - 360;
        else if (angle < -180)
            return angle + 360;
        return angle;
    }
}
