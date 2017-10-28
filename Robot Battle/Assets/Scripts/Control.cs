using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {
    new Rigidbody rigidbody;
    Vector3 moveDirection;
    Player player;
    CharacterController characterController;
    public float MouseSensitivity = 3;
    
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
	}
	
	void Update ()
    {
        #region Move
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
        #endregion

        #region Mouse

        var hor = Input.GetAxis("Mouse X");
        var ver = Input.GetAxis("Mouse Y");
        hor *= MouseSensitivity;
        ver *= MouseSensitivity;
        transform.Rotate(transform.up, hor);
        var hands = transform.Find("Wrap/Hands");
        hands.Rotate(0, ver, 0, Space.Self);
        Debug.DrawLine(hands.position, hands.position + hands.transform.localToWorldMatrix.MultiplyVector(Vector3.up) * 20, Color.blue);

        #endregion  

    }
}
