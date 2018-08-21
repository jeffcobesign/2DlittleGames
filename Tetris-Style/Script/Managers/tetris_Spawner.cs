using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetris_Spawner : MonoBehaviour {

    public tetris_Shape[] m_allShapes;

    public Transform[] m_queueXforms = new Transform[3];

    tetris_Shape[] m_queuedShapes = new tetris_Shape[3];
    float m_queueScale = 0.4f;



    tetris_Shape GetRandomShape()
    {
        int i = Random.Range(0, m_allShapes.Length);
        if(m_allShapes[i])
        {
            return m_allShapes[i];
        }
        else{
            Debug.LogWarning("WARNING! Invalid shape!");
            return null;
        }

    }

    public tetris_Shape SpawnShape()
    {
        tetris_Shape shape = null;
        //shape = Instantiate(GetRandomShape(), transform.position, Quaternion.identity) as tetris_Shape;
        shape = GetQueuedShape();
        shape.transform.position = transform.position;
        shape.transform.localScale = Vector3.one;

        if (shape) 
        { 
        return shape; 
        
        } else 
        { 
            Debug.LogWarning("WARNING! Invalid shape in spawner!");
            return null; 
        }
    }

    void InitQueue()
    {
        for (int i = 0; i < m_queuedShapes.Length; i++)
        {
            m_queuedShapes[i] = null;

        }
        FillQueue();
    }

    void FillQueue()
    {
        for (int i = 0; i < m_queuedShapes.Length; i++)
        {
            if(!m_queuedShapes[i])
            {
                m_queuedShapes[i] = Instantiate(GetRandomShape(), transform.position, Quaternion.identity) as tetris_Shape;
                m_queuedShapes[i].transform.position = m_queueXforms[i].position;
                m_queuedShapes[i].transform.localScale = Vector3.one * m_queueScale;
            }
        }
    }

    tetris_Shape GetQueuedShape()
    {
        tetris_Shape firstShape = null;
        if (m_queuedShapes[0])
        {
            firstShape = m_queuedShapes[0];
        }

        for (int i = 1; i < m_queuedShapes.Length; i++)
        {
            m_queuedShapes[i - 1] = m_queuedShapes[i];
            m_queuedShapes[i - 1].transform.position = m_queueXforms[i - 1].position;
        }

        m_queuedShapes[m_queuedShapes.Length - 1] = null;
        FillQueue();
        return firstShape;
    }


	// Use this for initialization
	void Start () {
        /*Vector2 originalVector = new Vector2(4.3f, 1.3f);
        Vector2 newVector = tetris_Vectorf.Round(originalVector);

        Debug.Log(newVector.ToString());
*/
        InitQueue();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
