using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speherecollider : MonoBehaviour
{

    [SerializeField]
    private GameObject[] m_checkPoint; // al the check points

    private int m_chechpointshad = 0; // how much check point you went troe



    private void Awake()
    {
        m_chechpointshad = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FinshTag>())
        {
            // if you went troe the finsh say you went troe it and go to chechpointactifate
            GameManager.M_Instance.SetWin();
            CheckPointActifate();

        }

        if(other.GetComponent<CheckPointTag>())
        {
            // if you went tro a checkpoint turn it off and go to chechpointchecker()
            other.gameObject.SetActive(false);            
            CheckPointChecker();
        }
    }

    private void CheckPointActifate()
    {
        // if you wnet troe start tel you have al the check point back turn the check point you had back to 0 and turn al the checkpoints back on
        GameManager.M_Instance.M_Checkpoints = true;
        m_chechpointshad = 0;
        for (int i = 0; i < m_checkPoint.Length; i++)
        {
            m_checkPoint[i].SetActive(true);
        }
    }

    private void CheckPointChecker()
    {
        // if you went trow a check point the say had the check point you had and check if you hade al the check point
        m_chechpointshad += 1;
        if (m_chechpointshad == m_checkPoint.Length)
        {
            GameManager.M_Instance.M_Checkpoints = false;
        }
    }

    private void Update()
    {
        if(GameManager.M_Instance.M_Checkpoints == false) 
        {
            // if there are no check point left turn start on 
            GameManager.M_Instance.Finish.SetActive(true);
        }
        else
        {
            // if there are check point left turn the start off 
            GameManager.M_Instance.Finish.SetActive(false);
        }
    }
}
