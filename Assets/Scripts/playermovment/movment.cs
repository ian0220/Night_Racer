using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{ // word niet gebruikt
    [SerializeField]
    private float m_Mass;
    private float m_MaxBochtNeem;
    private Rigidbody m_Rigidbody;

    [Header("curf")]
    private float m_BochtNeem;
    private float m_steeringangel;
    private float m_carangel;
    [SerializeField]
    private float m_LeftMaxCurfRotation;
    private float m_RightCurfRotation;
    [SerializeField]
    private float m_RightMaxCurfRotation;
    [SerializeField]
    private float m_CurgRotationSpeed;
    private bool m_IsRotaton = false;

    [Header("speed")]
    private float m_Speed;
    [SerializeField]
    private float m_MaxSpeed;
    [SerializeField]
    private float m_SpeedIncrease;
    [SerializeField]
    private float m_SpeedDecrees;

    private float m_MAxTimerRotaion = 0.5f;
    private float m_RotationRightTimer;
    private float m_MaxTimerSpeed = 1f;
    private float m_TimerSpeed; 


    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_RotationRightTimer = m_MAxTimerRotaion;
        m_TimerSpeed = m_MaxTimerSpeed;
    }

    private void Update()
    {
        Movment();
        TakingCurf();
    }

    private void FixedUpdate()
    {
        Debug.Log(m_Rigidbody.velocity.magnitude);
        if(m_Rigidbody.velocity.magnitude <= m_MaxSpeed)
        {
          m_Rigidbody.AddForce(transform.forward *Time.fixedDeltaTime * m_Speed * 1000f);
        }
        if(m_IsRotaton)
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0,m_steeringangel,0) * Time.fixedDeltaTime);
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        }
    }

    private void TakingCurf()
    {
       
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            m_IsRotaton = true;
           // if(m_steeringangel <= m_RightMaxCurfRotation)
           // {


                m_RotationRightTimer -= Time.deltaTime;
                if(m_RotationRightTimer <= 0)
                {
                    if(Input.GetKey(KeyCode.D))
                    {
                         Debug.Log(" rechts");
                         m_steeringangel += m_CurgRotationSpeed;

                    }
                    else
                    {
                        Debug.Log(" links");
                        m_steeringangel -= m_CurgRotationSpeed;
                    }

                    m_RotationRightTimer = m_MAxTimerRotaion;
                }
           // }
        }
        else
        {
            m_IsRotaton = false;
        }
    }

    private void Movment()
    {
        if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S))
        {
            m_TimerSpeed -= Time.deltaTime;
            if(Input.GetKey(KeyCode.W))
            {
                if(m_Speed <= m_MaxSpeed)
                {
                    if(m_TimerSpeed <= 0)
                    {
                        m_Speed += m_SpeedIncrease;
                        m_TimerSpeed = m_MaxTimerSpeed;
                    }
                }
                else
                {
                    m_Speed = m_MaxSpeed;
                }
            }
            else
            {
                if (m_Speed >= 0)
                {                 
                    if (m_TimerSpeed <= 0)
                    {
                        m_Speed -= m_SpeedDecrees;
                        m_TimerSpeed = m_MaxTimerSpeed;
                    }
                }
                else
                {
                    m_Speed = 0;
                } 
            }
        }
    }
}
