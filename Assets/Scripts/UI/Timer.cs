using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer M_Instance;
    public float M_Timer;
    public bool M_TimeStarted = false;
    [SerializeField] private Text m_TimerText;


    #region Singleton
    private void Awake()
    {
        if (M_Instance != null)
            Destroy(gameObject);
        else
            M_Instance = this;
    }
    #endregion


    void Update()
    {
        if (M_TimeStarted == true)
        {
            M_Timer += Time.deltaTime;
            Change();
        }
    }

    void Change()
    {
        
        string _Minutes = Mathf.Floor(M_Timer / 60).ToString("0");
        string _Seconds = (M_Timer % 60).ToString("00");

        m_TimerText.text = (_Minutes.ToString() + ":" + _Seconds.ToString());
    }
}
