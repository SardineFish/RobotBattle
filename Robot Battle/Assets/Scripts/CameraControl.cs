using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public float MouseSensitivity = 3;
    public float MaxVerticalAngle = 80;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        #region Mouse

        var hor = Input.GetAxis("Mouse X");
        var ver = -Input.GetAxis("Mouse Y");
        hor *= MouseSensitivity;
        ver *= MouseSensitivity;
        transform.Rotate(transform.up, hor);
        var camera = transform.Find("CameraWrap/Camera");
        var rotateTo = camera.localEulerAngles.x + ver;
        if(rotateTo>180)
            rotateTo -= 360;
		else if (rotateTo<-180)
            rotateTo += 360;
        //Debug.Log(rotateTo);
        if (rotateTo > MaxVerticalAngle)
        {
            ver = MaxVerticalAngle - camera.localEulerAngles.x;
        }
		if(rotateTo < -MaxVerticalAngle)
		{
            ver = -MaxVerticalAngle - camera.localEulerAngles.x;
		}
        camera.Rotate(ver, 0, 0, Space.Self);


        #endregion
    }
}
