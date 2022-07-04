using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject m_ScoreMenu;
    [SerializeField] private GameObject m_MainMenu;

    [SerializeField] private TextMeshProUGUI m_Lap1;
    [SerializeField] private TextMeshProUGUI m_Lap2;
    [SerializeField] private TextMeshProUGUI m_Lap3;
    [SerializeField] private TextMeshProUGUI m_HighScoreText;

    public void StartGame()
    {
        //When button is pressed load the game scene.
        SceneManager.LoadScene(1);
    }

    public void GoToScore()
    {
        m_MainMenu.SetActive(false);
        m_ScoreMenu.SetActive(true);
    }

    public void GoToMenu()
    {
        m_MainMenu.SetActive(true);
        m_ScoreMenu.SetActive(false);
    }

    private void Update()
    {
        Time.timeScale = 1f;
        m_HighScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore") + "s";
        m_Lap1.text = "Lap 1 " + PlayerPrefs.GetInt("Times" + 0) + "s";
        m_Lap2.text = "Lap 2 " + PlayerPrefs.GetInt("Times" + 1) + "s";
        m_Lap3.text = "Lap 3 " + PlayerPrefs.GetInt("Times" + 2) + "s";
    }
}
