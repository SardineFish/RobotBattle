using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {
    new Rigidbody rigidbody;
    Vector3 moveDirection;
    Player player;
    CharacterController characterController;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection -= transform.forward;
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += transform.right;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
        }
        moveDirection = moveDirection.normalized;
        //transform.Translate(player.Speed* Time.deltaTime * moveDirection);
        
        rigidbody.AddForce(moveDirection * player.MoveForce, ForceMode.Impulse);
        if (!player.OnGround)
        {
            if (Vector3.Dot(moveDirection, player.Velocity) < 0)
                rigidbody.AddForce(-player.Velocity.normalized * Vector3.Dot(moveDirection, -player.Velocity.normalized) * player.ForceFly, ForceMode.Impulse);
        }
        //var speed = moveDirection * player.Speed;
        //characterController.SimpleMove(speed);
	}
}
