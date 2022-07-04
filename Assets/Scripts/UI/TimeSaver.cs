using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSaver : MonoBehaviour
{
    static public TimeSaver M_Instance;

    [SerializeField] private float[] m_Times;
    public int M_TimeInt = 0;

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
        m_Times = new float[3];
        print(m_Times[M_TimeInt]);
    }

    public void ChangeTimes()
    {
        m_Times[M_TimeInt] = Mathf.RoundToInt(Timer.M_Instance.M_Timer);
        print(m_Times);
        PlayerPrefs.SetInt("Times" + M_TimeInt, Mathf.RoundToInt(Timer.M_Instance.M_Timer));
        PlayerPrefs.Save();
        Debug.Log("Time: "+ M_TimeInt +" "+ PlayerPrefs.GetInt("Times" + M_TimeInt));
    }
}
