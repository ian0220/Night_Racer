using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Game,
        Paused,
        Lost,
        Win
    }

    public static GameManager M_Instance;

    [Header("Debug")]
    [SerializeField] private GameState m_GameState;
    [SerializeField] private float m_FpsRefreshRate = 0.5f;
    [SerializeField] private bool m_FpsCheck;
    [SerializeField] private bool m_DeletePref;
    [SerializeField] private int m_LapsDriven;
    [SerializeField] private ParticleSystem m_Particle;
    [SerializeField] private Text m_LapsDrivenText;


    private float m_TimerTime;
    private HighscoreSystem m_Highscore;
    private InterfaceManager m_InterFace;
    private Timer m_Timer;
    public bool M_IsCountingDown = true;
    public bool M_Checkpoints = true;
    public GameObject Finish;
    #region Singleton
    private void Awake()
    {
        if (M_Instance != null)
            Destroy(gameObject);
        else
            M_Instance = this;
    }
    #endregion

    void Start()
    {
        m_InterFace = InterfaceManager.M_Instance;
        m_Highscore = HighscoreSystem.M_Instance;
        m_Timer = Timer.M_Instance;
    }

    void Update()
    {
        //Will check if the Gamestate is game or paused when it is
        //It will then check if the pause will be activated
        if (m_GameState == GameState.Game || m_GameState == GameState.Paused)
            CheckPause();

        //Will call the update fuction
        if (m_FpsCheck == true)
            UpdateFPS();

        if (m_LapsDriven == 4)
        {
            
            m_InterFace.ShowRestartMenu();
            Time.timeScale = 0f;
        }

        TextUpdate();

        if (m_DeletePref == true)
            DeletePlayerpref();
    }

    private void TextUpdate()
    {
        if(m_LapsDriven < 4)
            m_LapsDrivenText.text = m_LapsDriven.ToString() + "/3";
    }

    /// <summary>
    /// This updates your fps
    /// Only for debugging
    /// </summary>
    private void UpdateFPS()
    {
        //Will update the fps when is active
        if (Time.unscaledTime > m_TimerTime)
        {
            int _Fps = (int)(1f / Time.unscaledDeltaTime);
            Debug.Log(_Fps);
            m_TimerTime = Time.unscaledTime + m_FpsRefreshRate;
        }
    }
    private void DeletePlayerpref()
    {
        PlayerPrefs.DeleteKey("Highscore");
    }

    public void SetGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        SetGameState(GameState.Lost);
        //Game Over screen comes here
    }
    /// <summary>
    /// Sets the new gamestate in this function
    /// </summary>
    /// <param name="_state"> The new state name </param>
    public void SetGameState(GameState _state)
    {
        m_GameState = _state;
    }

    /// <summary>
    /// ChechPause will check if the game is paused
    /// So yes it will be put on hold 
    /// If not it will not be put on hold
    /// </summary>
    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && m_GameState != GameState.Lost)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (m_GameState == GameState.Paused)
            {
                // Interface show pause false
                SetGameState(GameState.Game);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                Cursor.visible = false;
                Debug.Log("Game is Unpaused");
            }
            else if (m_GameState == GameState.Game)
            {
                // inter face show pause true
                SetGameState(GameState.Paused);
                Time.timeScale = 0f;
                Debug.Log("Game is Paused");
            }
        }
    }

    /// <summary>
    /// Will start the game with the timer and lapsdriven
    /// </summary>
    public void StartGame()
    {
        M_IsCountingDown = false;
        m_Timer.M_TimeStarted = true;
        m_LapsDriven++;
        m_Particle.Play();
    }

    /// <summary>
    /// Set the win.
    /// Time will be stopped and will be saved in the timesaver script
    /// Highscore will change if it has a new highscore
    /// </summary>
    public void SetWin()
    {
        m_Timer.M_TimeStarted = false;

        // TimeSaver
        TimeSaver.M_Instance.ChangeTimes();
        TimeSaver.M_Instance.M_TimeInt++;

        // Change highscore if the score is above that
        if (m_Highscore.M_HighScore == 0)
            m_Highscore.HighScoreChanger(Mathf.RoundToInt(m_Timer.M_Timer));
        else if (m_Timer.M_Timer < m_Highscore.M_HighScore)
            m_Highscore.HighScoreChanger(Mathf.RoundToInt(m_Timer.M_Timer));
        m_Timer.M_Timer = 0;
        StartGame();

    }

    // Time Death for the end screen
    // Will stop the time so the end screen can be shown
    public void TimeDeath()
    {
        m_Timer.M_TimeStarted = false;
        Time.timeScale = 0f;
    }
}
