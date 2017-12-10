using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShakeCamera : MonoBehaviour {
    public bool Enable = false;
    public float Range = 1;
    public float Strength = 1;
    public float MaxRange = 1;
    public float ShakeIncreasement = 0.3f;
    public float ShakeDecreasement = 0.5f;
    public float MaxCrosshair = 2;
    public float CrosshairScale = 1;
    public float ShakeX;
    public float ShakeY;
    public GameObject Crosshair;
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Enable)
        {
            
            var x = Mathf.PerlinNoise(Time.time * Strength, 1);
            var y = Mathf.PerlinNoise(1, Time.time * Strength);
            x -= 0.5f;
            y -= 0.5f;
            x *= Range;
            y *= Range;
            //transform.localPosition = new Vector3(x, y, 0);
            ShakeX = x;
            ShakeY = y;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Range += ShakeIncreasement;
                if (Range > MaxRange)
                    Range = MaxRange;
            }
            else
            {
                Range -= ShakeDecreasement;
                if (Range < 0)
                    Range = 0;
            }
            CrosshairScale = (Range / MaxRange) * (MaxCrosshair - 1) + 1;
            Crosshair.GetComponent<RectTransform>().localScale = new Vector3(CrosshairScale, CrosshairScale, 1);
        }
	}
}
