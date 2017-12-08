using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float MouseSensitivity = 3;
    public float MaxVerticalAngle = 80;
    public GameObject ControllingGameObject;
    public Player ControllingPlayer;
    new GameObject camera;
    private ShakeCamera shakeCamera;

    // Use this for initialization
    void Start ()
    {
        camera = transform.Find("CameraWrap/Camera").gameObject;
        shakeCamera = GetComponent<ShakeCamera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameSystem.Current.GameStarted)
            return;
	    if (ControllingGameObject)
	        ControllingPlayer = ControllingGameObject.GetComponent<Player>();
	    else
	        return;
        #region Mouse

        var hor = Input.GetAxis("Mouse X");
        var ver = -Input.GetAxis("Mouse Y");
        hor *= MouseSensitivity;
        ver *= MouseSensitivity;

        hor += shakeCamera.ShakeX;
	    ver += shakeCamera.ShakeY;

        transform.Rotate(transform.up, hor);
        var rotateTo = camera.transform.localEulerAngles.x + ver;
        if(rotateTo>180)
            rotateTo -= 360;
		else if (rotateTo<-180)
            rotateTo += 360;
        //Debug.Log(rotateTo);
        if (rotateTo > MaxVerticalAngle)
        {
            ver = MaxVerticalAngle - camera.transform.localEulerAngles.x;
        }
		if(rotateTo < -MaxVerticalAngle)
		{
            ver = -MaxVerticalAngle - camera.transform.localEulerAngles.x;
		}
        camera.transform.Rotate(ver, 0, 0, Space.Self);
        transform.position = ControllingPlayer.transform.position;

        Debug.DrawLine(camera.transform.position, camera.transform.position + camera.transform.forward * 100, Color.black);
        ControllingPlayer.LookAt(new Ray(camera.transform.position, camera.transform.forward));
        #endregion
    }
}
