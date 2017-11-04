using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {
    new Rigidbody rigidbody;
    Vector3 moveDirection;
    Player player;
    CharacterController characterController;
    public GameObject ControlCamera = null;

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
        player.MoveDirection = moveDirection;
        //player.Move(moveDirection);
        var v = Vector3.Scale(player.Velocity, new Vector3(1, 0, 1));
        if (!player.OnGround)
        {
            if (Vector3.Dot(moveDirection, v) < 0)
                rigidbody.AddForce(-v.normalized * Vector3.Dot(moveDirection, -v.normalized) * player.ForceFly, ForceMode.Impulse);
        }
        #endregion

        if (Input.GetKey(KeyCode.Mouse0))
        {
            player.Fire();
        }

        #region FollowCamera

        if (ControlCamera)
        {
            var forward = ControlCamera.transform.forward;
            player.LookAt(forward);

        }

        #endregion

    }
}
