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
        var v = Vector3.Scale(player.Velocity, new Vector3(1, 0, 1));
        if (!player.OnGround)
        {
            if (Vector3.Dot(moveDirection, v) < 0)
                rigidbody.AddForce(-v.normalized * Vector3.Dot(moveDirection, -v.normalized) * player.ForceFly, ForceMode.Impulse);
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

        if (Input.GetKey(KeyCode.Mouse0))
        {
            var gunLeft = transform.Find("Wrap/Hands/Gun-L/Gun-Inside").gameObject;
            var gunRight = transform.Find("Wrap/Hands/Gun-R/Gun-Inside").gameObject;
            var rayL = new Ray(gunLeft.transform.position, -gunLeft.transform.right);
            var rayR = new Ray(gunRight.transform.position, -gunRight.transform.right);
            Debug.DrawLine(rayL.origin, rayL.origin + rayL.direction * 100, Color.red);
            Debug.DrawLine(rayR.origin, rayR.origin + rayR.direction * 100, Color.red);
            

        }

        

        #endregion  

    }
}
