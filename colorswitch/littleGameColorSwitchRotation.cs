using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleGameColorSwitchRotation : MonoBehaviour {

    public float rotationspeed = 100f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0f, 0f, rotationspeed * Time.deltaTime);
		
	}
}
