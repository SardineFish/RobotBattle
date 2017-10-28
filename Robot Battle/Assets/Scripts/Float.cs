using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {
    public float Height;
    public float k = 1;
    new Rigidbody rigidbody;
	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * hit.distance, Color.red);
            if (hit.distance <= Height)
            {
                var dh = Height - hit.distance;
                var f = -Physics.gravity * (rigidbody.mass + dh * k);
                //rigidbody.AddForce(f, ForceMode.Impulse);
            }
        }
    }
}
