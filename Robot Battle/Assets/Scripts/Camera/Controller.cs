using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Controller: MonoBehaviour
{
    public GameObject Target;
    private ActionController actionController;
    private CameraController cameraController;
    private CameraFollow cameraFollow;

    void Start()
    {
        actionController = GetComponent<ActionController>();
        cameraController = GetComponent<CameraController>();
        cameraFollow = GetComponent<CameraFollow>();
    }

    public void SetTarget(GameObject target)
    {
        Target = target;
        actionController.ControllingGameObject = target;
        cameraController.ControllingGameObject = target;
        cameraFollow.FollowTarget = target;
    }

}