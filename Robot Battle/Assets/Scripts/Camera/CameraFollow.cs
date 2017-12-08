using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public GameObject FollowTarget = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
	[ExecuteInEditMode]
    void Update()
    {
        if (FollowTarget)
        {
            transform.position = FollowTarget.transform.position;
        }
    }
}
