using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetris_Ghost : MonoBehaviour {

    tetris_Shape m_ghostShape = null;
    bool m_hitBottom = false;

    public Color m_color = new Color(1f, 1f, 1f, 0.2f);

    public void DrawGhost(tetris_Shape originalShape, tetris_Board gameBoard)
    {
        if(!m_ghostShape)
        {
            m_ghostShape = Instantiate(originalShape, originalShape.transform.position, originalShape.transform.rotation) as tetris_Shape;
            m_ghostShape.gameObject.name = "GhostShape";

            SpriteRenderer[] allRenders = m_ghostShape.GetComponentsInChildren<SpriteRenderer>();

            foreach(SpriteRenderer r in allRenders)
            {
                r.color = m_color;

            }

        }else
        {
            m_ghostShape.transform.position = originalShape.transform.position;
            m_ghostShape.transform.rotation = originalShape.transform.rotation;
        }

        m_hitBottom = false;

        while(!m_hitBottom)
        {
            m_ghostShape.MoveDown();
            if(!gameBoard.IsValidPosition(m_ghostShape))
            {
                m_ghostShape.MoveUp();
                m_hitBottom = true;
            }
        }
    }

    // Use this for initialization
    public void Reset()
    {
        Destroy(m_ghostShape.gameObject);
    }
}
