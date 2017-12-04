using Assets.Scripts.AI.Goals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {
    new Rigidbody rigidbody;
    Vector3 moveDirection;
    public Player ControllingPlayer;
    public GameObject ControllingGameObject;

    void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        if (!GameSystem.Now.GameStarted)
            return;
        if (ControllingGameObject)
            ControllingPlayer = ControllingGameObject.GetComponent<Player>();
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
            ControllingPlayer.Jump();
        }
        moveDirection = moveDirection.normalized;
        //transform.Translate(player.Speed* Time.deltaTime * moveDirection);
        ControllingPlayer.Move(moveDirection);
        transform.position = ControllingPlayer.transform.position;
        //player.Move(moveDirection);
        #endregion

        if (ControllingPlayer.RobotAssistant)
        {
            var assistant = ControllingPlayer.RobotAssistant.GetComponent<Player>();
            if (Input.GetKeyDown(KeyCode.F))
            {
                assistant.GoalsManager.Goals.Clear();
                assistant.GoalsManager.AddGoal(new Follow(assistant, ControllingPlayer, 40));
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            ControllingPlayer.Fire();
        }

    }
}
