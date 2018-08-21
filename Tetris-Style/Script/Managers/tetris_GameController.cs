using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetris_GameController : MonoBehaviour {

    tetris_Board m_gameBoard;
    tetris_Spawner m_spawner;

    tetris_Shape m_activeShape;

    tetris_Ghost m_ghost;


    [Range(0.02f, 1f)]
    public float m_dropInterval = 0.01f;
    float m_dropIntervalModded;

    float m_timeToDrop;

    float m_keyToNextKey;
    /*
    float m_timeToNextKey;

    [Range (0.02f,1f)]
    public float m_keyRepeatRate = 0.25f;
    */
    float m_timeToNextKeyLeftRight;

    [Range(0.02f, 1f)]
    public float m_keyRepeatRateLeftRight = 0.25f;
    float m_timeToNextKeyDown;

    [Range(0.01f, 1f)]
    public float m_keyRepeatRateDown = 0.25f;
    float m_timeToNextKeyRotate;

    [Range(0.02f, 1f)]
    public float m_keyRepeatRateRotate = 0.25f;


    bool m_gameOver = false;

    public tetris_IconToggle m_rotateIconToggle;

    bool m_clockwise = true;




    public GameObject m_gameOverPanel;
    public GameObject m_gamePausePanel;

    tetris_SoundManager m_soundManager;
    tetris_ScoreManager m_scoreManager;

    public bool m_isPaused = false;


	// Use this for initialization
	void Start () 
    {
        //m_gameBoard = GameObject.FindWithTag("Board").GetComponent<tetris_Board>();
        // m_gameBoard = GameObject.FindObjectsOfType<tetris_Board>();

        m_timeToNextKeyLeftRight = Time.time+m_keyRepeatRateLeftRight;
        m_timeToNextKeyDown = Time.time + m_keyRepeatRateDown;
        m_timeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;

        m_gameBoard = GameObject.FindObjectOfType(typeof(tetris_Board)) as tetris_Board;
        m_spawner = GameObject.FindWithTag("Spawner").GetComponent<tetris_Spawner>();

        m_soundManager= GameObject.FindObjectOfType<tetris_SoundManager>();

        m_scoreManager= GameObject.FindObjectOfType<tetris_ScoreManager>();

        m_ghost = GameObject.FindObjectOfType<tetris_Ghost>();

        if (!m_scoreManager)
        {
            Debug.LogWarning("WARNING!   There is no scoreManager defined!");
        }
        if(!m_soundManager)
        {
            Debug.LogWarning("WARNING!   There is no soundManager defined!");
        }

        if (!m_gameBoard) 
        {
            Debug.Log("WARNING!    There is no game board defined"); 
        
        }

        if(!m_spawner)
        { 
            Debug.Log("WARNING!    There is no game spawner defined"); 
        
        }else
        {
            m_spawner.transform.position = tetris_Vectorf.Round(m_spawner.transform.position);
            if (!m_activeShape)
            {
                m_activeShape = m_spawner.SpawnShape();

            }

        }

        if (m_gameOverPanel)
        {
            m_gameOverPanel.SetActive(false);
        }

        if (m_gamePausePanel)
        {
            m_gamePausePanel.SetActive(false);
        }

        m_dropIntervalModded = m_dropInterval;
       
	}
	

    void PlayerInput()
    {
        if ((Input.GetButton("MoveRight") && (Time.time > m_timeToNextKeyLeftRight)) || Input.GetButtonDown("MoveRight"))
        {
            m_activeShape.MoveRight();
            m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;


            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveLeft();
                PlaySound(m_soundManager.m_errorSound,0.5f);
            }else
            {
                PlaySound(m_soundManager.m_moveSound,0.5f);
                
            }
        }

        else if ((Input.GetButton("MoveLeft") && (Time.time > m_timeToNextKeyLeftRight)) || Input.GetButtonDown("MoveLeft"))
        {
            m_activeShape.MoveLeft();
            m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
   


            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveRight();
                PlaySound(m_soundManager.m_errorSound, 0.5f);
            }
            else
            {
                PlaySound(m_soundManager.m_moveSound, 0.5f);

            }
        }

        else if (Input.GetButton("Rotate") && (Time.time > m_timeToNextKeyRotate))
        {
            // m_activeShape.RotateRight();

            m_activeShape.RotateClockwise(m_clockwise);
            m_timeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                //m_activeShape.RotateLeft();
                m_activeShape.RotateClockwise(!m_clockwise);
                PlaySound(m_soundManager.m_moveSound, 0.5f);
            }
        }

        else if ((Input.GetButton("MoveDown") && (Time.time > m_timeToNextKeyDown)) || (Time.time > m_timeToDrop))
        {
            m_timeToDrop = Time.time + m_dropIntervalModded;
            m_timeToNextKeyDown = Time.time + m_keyRepeatRateDown;

            m_activeShape.MoveDown();
            PlaySound(m_soundManager.m_moveSound, 0.01f);

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                if (m_gameBoard.IsOverLimit(m_activeShape))
                {
                    GameOver();

                }
                else
                {

                    LandShape();
                }
            }
        }
        else if(Input.GetButtonDown("ToggleRot"))
        {
            ToggleRotDirection();
        }
        else if(Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }



    }

    private void PlaySound(AudioClip clip, float volMultiplier)
    {
        if (m_soundManager.m_fxEnabled && clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, Mathf.Clamp(m_soundManager.m_fxVolume * volMultiplier,0.05f,1f));
        }
    }

    private void GameOver()
    {
        m_activeShape.MoveUp();
        m_gameOver = true;
        if (m_gameOverPanel)
        {
            m_gameOverPanel.SetActive(true);
        }
        PlaySound(m_soundManager.m_gameOverSound, 5f);
        PlaySound(m_soundManager.m_gameOverVocalClip, 5f);

        Debug.LogWarning(m_activeShape.name + " is over the limit height!");
    }

    private void LandShape()
    {
        // shape lands here

        m_timeToNextKeyLeftRight = Time.time;
        m_timeToNextKeyDown = Time.time;
        m_timeToNextKeyRotate = Time.time;

        m_activeShape.MoveUp();
        m_gameBoard.StoreShapeInGrid(m_activeShape);

        if (m_ghost) { m_ghost.Reset(); }

        m_activeShape = m_spawner.SpawnShape();
        m_gameBoard.ClearAllRows();

        PlaySound(m_soundManager.m_dropSound,0.5f);

        if(m_gameBoard.m_completedRows >0)
        {
            m_scoreManager.ScoreLines(m_gameBoard.m_completedRows);

            if(m_scoreManager.m_didLevelUp)
            {
                PlaySound(m_soundManager.m_levelUpVocalClip, 1f);
                m_dropIntervalModded = Mathf.Clamp( m_dropInterval - (((float)m_scoreManager.m_level - 1)*0.03f), 0.05f, 1f);


            }else
            {
                if (m_gameBoard.m_completedRows > 1)
                {
                    AudioClip randomVocal = m_soundManager.GetRandomClip(m_soundManager.m_vocalClips);
                    PlaySound(randomVocal,0.5f);
                } 
            }



            PlaySound(m_soundManager.m_clearRowSound,0.7f);
        }

    }

    // Update is called once per frame
    void Update()
    {



        //if don't have spawner or gameBoard just don't run the game;
        if (!m_gameBoard || !m_spawner || !m_activeShape || m_gameOver || !m_soundManager || !m_scoreManager)
        {
            return;
        }

        PlayerInput();
	}

    public void Restart()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ToggleRotDirection()
    {
        m_clockwise = !m_clockwise;
        if(m_rotateIconToggle)
        {
            m_rotateIconToggle.ToggleIcon(m_clockwise);
        }
    }


    private void LateUpdate()
    {
        if(m_ghost)
        {
            m_ghost.DrawGhost(m_activeShape, m_gameBoard);
        }
    }

    public void TogglePause()
    {
        if (m_gameOver) { return; }

        m_isPaused = !m_isPaused;

        if (m_gamePausePanel)
        {
            m_gamePausePanel.SetActive(m_isPaused);
            if(m_soundManager)
            {
                m_soundManager.m_musicSource.volume = (m_isPaused) ? m_soundManager.m_musicVolume * 0.25f : m_soundManager.m_musicVolume;
            }
            Time.timeScale = (m_isPaused) ? 0 : 1;
        }
    }

}
