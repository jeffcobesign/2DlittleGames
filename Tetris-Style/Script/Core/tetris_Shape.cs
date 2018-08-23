using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetris_Shape : MonoBehaviour {

    public bool m_canRotate = true;
    public Vector3 m_queueOffset;

    GameObject[] m_glowSquareFx;
    public string glowSquareTag = "LandShapeFx";

    void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection;

    }

    public void MoveLeft() { Move(new Vector3(-1, 0, 0)); }
    public void MoveRight() { Move(new Vector3(1, 0, 0)); }
    public void MoveUp() { Move(new Vector3(0, 1, 0)); }
    public void MoveDown() { Move(new Vector3(0,-1, 0)); }

    public void RotateRight() 
    {
        if (m_canRotate) 
        { 
            transform.Rotate(0, 0, -90); 
        }
    }
    public void RotateLeft() 
    { 
        if (m_canRotate)
        {
            transform.Rotate(0, 0, 90);
        }
    }

    public void RotateClockwise(bool clockwise)
    {
        if (clockwise)
        {
            RotateRight();
        }
        else { RotateLeft(); }
    }

    // Use this for initialization
    void Start () {
        // InvokeRepeating("MoveDown", 0, 0.5f);
        //  InvokeRepeating("RotateLeft", 0, 2f);
        if (glowSquareTag != "")
        {
            m_glowSquareFx = GameObject.FindGameObjectsWithTag(glowSquareTag);
        }
	}

    public void LandShapeFX()
    {
        int i = 0;

        foreach(Transform child in gameObject.transform)
        {
            if(m_glowSquareFx[i])
            {
                m_glowSquareFx[i].transform.position = new Vector3(child.position.x, child.position.y, -2.5f);
                tetris_ParticlePlayer particlePlayer = m_glowSquareFx[i].GetComponent<tetris_ParticlePlayer>();

                if (particlePlayer)
                {
                    particlePlayer.Play();
                }
                i++;
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
