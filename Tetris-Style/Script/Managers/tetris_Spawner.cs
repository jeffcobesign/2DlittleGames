using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetris_Spawner : MonoBehaviour {

    public tetris_Shape[] m_allShapes;

    public Transform[] m_queueXforms = new Transform[3];

    tetris_Shape[] m_queuedShapes = new tetris_Shape[3];
    float m_queueScale = 0.4f;
    public tetris_ParticlePlayer m_spawnFx;


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
        //shape.transform.localScale = Vector3.one;

        StartCoroutine(GrowShape(shape, transform.position, 0.30f));

        if(m_spawnFx)
        {
            m_spawnFx.Play();
        }
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
                m_queuedShapes[i].transform.position = m_queueXforms[i].position+ m_queuedShapes[i].m_queueOffset;
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
            m_queuedShapes[i - 1].transform.position = m_queueXforms[i - 1].position + m_queuedShapes[i].m_queueOffset;
        }

        m_queuedShapes[m_queuedShapes.Length - 1] = null;
        FillQueue();
        return firstShape;
    }


	// Use this for initialization
	void Awake () {
        /*Vector2 originalVector = new Vector2(4.3f, 1.3f);
        Vector2 newVector = tetris_Vectorf.Round(originalVector);

        Debug.Log(newVector.ToString());
*/
        InitQueue();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GrowShape(tetris_Shape shape, Vector3 position, float growTime = 0.5f)
    {
        float size = 0f;
        growTime = Mathf.Clamp(growTime, 0.1f, 2f);
        // 1 units/seconds  *  seconds/frame = units / frame
        float sizeDelta =  Time.deltaTime / growTime;

        while(size<1f)
        {
            shape.transform.localScale = Vector3.one * size;
            size += sizeDelta;
            shape.transform.position = position;
            yield return null;  //wait until the next frame

        }

        shape.transform.localScale = Vector3.one;
    }
}
