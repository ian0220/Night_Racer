using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    //UI Objects
    [Header("UI Elements")]
    [SerializeField]
    private Text M_CountdownText;
    [SerializeField]
    private GameObject M_CountDownImage;
    [SerializeField]
    private GameObject M_CountdownUI;
    [SerializeField]
    private GameObject M_Needle;
    [SerializeField]
    private GameObject M_RestartMenu;

    //Placeholder for velocity for debugging.
    [Header("Testing")]
    public float M_VehicleSpeed;
    [SerializeField]
    private float M_MaxVehicleSpeed = 280;

    #region Singleton
    public static InterfaceManager M_Instance;

    private void Awake()
    {
        if (M_Instance != null)
            Destroy(gameObject);
        else
            M_Instance = this;
    }
    #endregion

    #region Countdown Coroutine
    public void Start()
    {
        M_RestartMenu.SetActive(false);
        //Start countdown coroutine.
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        //Display a countdown from 3 till the round starts.
        yield return new WaitForSeconds(0.3f);
        AudioManger.M_Instance.PLay("Aftellen");
        M_CountdownText.text= "3";
        yield return new WaitForSeconds(1);
        M_CountdownText.text = "2";
        yield return new WaitForSeconds(1);
        M_CountdownText.text = "1";
        yield return new WaitForSeconds(1);
        M_CountdownText.text = "Go!";
        GameManager.M_Instance.StartGame();
        yield return new WaitForSeconds(1);
        M_CountDownImage.GetComponent<Image>().CrossFadeAlpha(0.1f, 0.3f, false);
        yield return new WaitForSeconds(0.3f);
        M_CountdownUI.SetActive(false);
    }
    #endregion

    #region Update Speedometer
    private void Update()
    {
        //Update speedometer every frame.
        UpdateSpeedometer();
    }

    private void UpdateSpeedometer()
    {
        //min and max rotation positions of the needle.
        float _startPosition = 96, _endPosition = -98;
        float _desiredPosition = _startPosition - _endPosition;

        //Position and clamp the needle of the speedometer towards the current velocity of the player
        float _temp = (M_VehicleSpeed * 12) / M_MaxVehicleSpeed;
        float _rotationSpeed = 10;

        //M_Needle.transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp((_startPosition - _temp * _desiredPosition), _endPosition, _startPosition));
        M_Needle.transform.rotation = Quaternion.Lerp(M_Needle.transform.rotation, Quaternion.Euler(new Vector3(0, 0, Mathf.Clamp((_startPosition - _temp * _desiredPosition), _endPosition, _startPosition))), _rotationSpeed * Time.deltaTime); 
    }
    #endregion

    #region RestartMenu
    public void ShowRestartMenu()
    {
        // Show the restart menu
        M_RestartMenu.SetActive(true);
    }

    public void RestartGame()
    {
        // Restart the game by loading the ingame scene.
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        //Go to men ufrom the restart menu
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    #endregion




}