using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreSystem : MonoBehaviour
{
    public static HighscoreSystem M_Instance;

    [Header("Timer system")]
    public int M_HighScore;

    #region Singleton
    private void Awake()
    {
        if (M_Instance != null)
            Destroy(gameObject);
        else
            M_Instance = this;
    }
    #endregion

    private void Start()
    {
        // Displays the highscore on the screen
        M_HighScore = PlayerPrefs.GetInt("HighScore");
    }

    

    public void HighScoreChanger(int _Time)
    {
        M_HighScore = _Time;

        AudioManger.M_Instance.PLay("NewHighScore");
        //changes the highscore player prefs
        PlayerPrefs.SetInt("HighScore", M_HighScore);
        PlayerPrefs.Save();
        Debug.Log(M_HighScore);
    }

}
