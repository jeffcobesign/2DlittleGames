using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleGamecutRopeCutScript : MonoBehaviour {

    public Camera cutRopeCam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit2D cutRopeRay = Physics2D.Raycast(cutRopeCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(cutRopeRay.collider!=null)
            {
                if (cutRopeRay.collider.tag == "Rope")
                    Destroy(cutRopeRay.collider.gameObject);
            }
        }
        
		
	}
}
