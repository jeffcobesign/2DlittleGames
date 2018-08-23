using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class tetris_SettingsPanel : MonoBehaviour {

    tetris_GameController m_gameController;
    tetris_TouchController m_touchController;

    public Slider m_dragDistanceSlider;
    public Slider m_swipeDistanceSlider;
    public Slider m_dragSpeedSlider;

    public Toggle m_toggleDiagnostic;


	// Use this for initialization
	void Start () {
        m_gameController = GameObject.FindObjectOfType<tetris_GameController>().GetComponent<tetris_GameController>();
        m_touchController = GameObject.FindObjectOfType<tetris_TouchController>().GetComponent<tetris_TouchController>();

        if(m_dragDistanceSlider != null)
        {
            m_dragDistanceSlider.value = 100;
            m_dragDistanceSlider.minValue = 50;
            m_dragDistanceSlider.maxValue = 150;
        }

        if(m_swipeDistanceSlider != null)
        {
            m_swipeDistanceSlider.value = 50;
            m_swipeDistanceSlider.minValue = 20;
            m_swipeDistanceSlider.maxValue = 250;
        }

        if(m_dragSpeedSlider !=null && m_gameController !=null)
        {
            m_dragSpeedSlider.value = 0.15f;
            m_dragSpeedSlider.minValue = 0.05f;
            m_dragSpeedSlider.maxValue = 0.5f;
        }

        if(m_toggleDiagnostic != null && m_touchController != null)
        {
            m_touchController.m_useDiagnostic = m_toggleDiagnostic.isOn;
        }

	}

    public void UpdatePanel()
    {
        if(m_dragDistanceSlider !=null && m_touchController !=null)
        {
            m_touchController.m_minDragDistance = (int)m_dragDistanceSlider.value;

        }

        if(m_touchController !=null && m_gameController != null)
        {
            m_touchController.m_minSwipeDistance = (int)m_swipeDistanceSlider.value;
        }

        if(m_toggleDiagnostic!=null && m_touchController !=null)
        {
            m_touchController.m_useDiagnostic = m_toggleDiagnostic.isOn;

        }

        if (m_dragSpeedSlider != null && m_gameController != null)
        {
            m_gameController.m_minTimeToDrag = m_dragSpeedSlider.value;
        }
    }
}
