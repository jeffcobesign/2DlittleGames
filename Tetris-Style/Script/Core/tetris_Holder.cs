using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetris_Holder : MonoBehaviour {

    public Transform m_holderXform;
    public tetris_Shape m_heldShape = null;
    public bool m_canRelease = false;

    float m_scale = 0.4f;


    public void Catch(tetris_Shape shape)
    {
        if(m_heldShape)
        {
            Debug.LogWarning("HOLDER WARNING:    Release a shape before trying to hold!");
            return;
        }

        if(!shape)
        {
            Debug.LogWarning("HOLDER WARNING!  Invalid shape!");
            return;
        }

        if(m_holderXform)
        {
            shape.transform.position = m_holderXform.position + shape.m_queueOffset;
            shape.transform.localScale = Vector3.one * m_scale;
            m_heldShape = shape;
            shape.transform.rotation = Quaternion.identity;

        }
        else
        {
            Debug.LogWarning("HOLDER WARNING!   Holder has no transform assigned");
        }
    }

    public tetris_Shape Release()
    {
        m_heldShape.transform.localScale = Vector3.one;
        tetris_Shape shape = m_heldShape;

        m_heldShape = null;

        m_canRelease = false;

        return shape;

    }
}
