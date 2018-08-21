using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleGameColorSwitchCameraFollow : MonoBehaviour {

    public Transform Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Player.position.y>transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, Player.position.y, transform.position.z);

        }
	}
}
