using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // https://youtu.be/cqATTzJmFDY is used to make this script
    public Rigidbody M_TheRB; // rigidbody that you move

    [SerializeField]
    private SchripobjectPlayerMovment m_SchripobjectPlayerMovment; // scriptebelobject
    
    private float m_SpeedInput, m_TurnInput; // the input of your control

    private bool m_Ground; // to check if you are on the ground
    [Header("raycast")]
    public LayerMask M_WhatGround; // what layer you want to shoor with your raccast 
    public float M_GroundRayLenght; // how far can the raycast shoor
    public Transform M_GroundRayPoint; // from where has the raycast to shoot "where is my gun"

    [Header("take a curf")]
    [SerializeField]
    private GameObject m_RacerObject;// what objects rotation you wnats to turn 
    [SerializeField]
    private float m_Rotation = 20;// how far the rotation to do if you turn
    [SerializeField]
    private float m_Normalrotation = 0; // what rotation to if you dont turn
    [SerializeField]
    private float m_lerptime = 20; // how fast to lerp to the rotation to turn

    void Start()
    {
        // the spehere gets of the parent and get on its on      
        M_TheRB.transform.parent = null; 
    }


    void Update()
    {
        
        m_SpeedInput = 0f;
        // is it counted down 
        if(!GameManager.M_Instance.M_IsCountingDown)
        { 
            // check if you want to go forward or backward
             if (Input.GetAxis("Vertical") > 0)
             {    
                // kijkt of je rechtin drukt en voeft de acelaration speed to * 1000 
                 m_SpeedInput = Input.GetAxis("Vertical") * m_SchripobjectPlayerMovment.M_ForwardAccel *  1000;
             }
             else if (Input.GetAxis("Vertical") < 0)
             { 
                 m_SpeedInput = Input.GetAxis("Vertical") * m_SchripobjectPlayerMovment.M_ReverseAccel *  1000;
             }

             // put in m_turninput where you want to turn
             m_TurnInput = Input.GetAxis("Horizontal");
            // what to do if you turn
             if(m_TurnInput > 0)
             {
                 RotatingToRight();
             }
             else if(m_TurnInput == 0)
             {
                 RotatingToNormal();
             }
             else if(m_TurnInput < 0)
             {
                 RotatingToLeft();
             }

        }
        // how to turn
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, m_TurnInput * m_SchripobjectPlayerMovment.M_TurnStrength * Time.deltaTime, 0f));
        // place your gameobject to the postion of your former child 
        transform.position = M_TheRB.transform.position;

    }

    private void FixedUpdate()
    {
        m_Ground = false;
        RaycastHit _hit;

        // check if you are on the ground with a ray cast that checks the layer "floor"
        if (Physics.Raycast(M_GroundRayPoint.position, -transform.up, out _hit,M_GroundRayLenght,M_WhatGround))
        { 
            m_Ground = true;
        }

        // ckecks if you are on the ground
        if (m_Ground) 
        {
            // if on the ground pot the drag to the drag of normaledrag
            M_TheRB.drag = m_SchripobjectPlayerMovment.M_NormalDrag;
            if (Mathf.Abs(m_SpeedInput) > 0 && M_TheRB.velocity.magnitude <= m_SchripobjectPlayerMovment.M_MaxSpeed)
            {
                // if your input is somving more then 0 and your speed is below the max speed you can adforce so that you can go faster
                 M_TheRB.AddForce(transform.forward * m_SpeedInput);
                 AudioManger.M_Instance.PLay("Car");
            }
            else if(m_SpeedInput == 0)
            {
                AudioManger.M_Instance.Stop("Car");
            }
        }
        else
        { 
            // if you are not on the ground pot the player drag to de flydrag and turn the grafity up so that you go down faster
            M_TheRB.drag = m_SchripobjectPlayerMovment.M_FlyDrag;
            M_TheRB.AddForce(Vector3.up * -m_SchripobjectPlayerMovment.M_GraftyFalls * 100f);
        }
        // tel what your speed is to the interfacemanger
        InterfaceManager.M_Instance.M_VehicleSpeed = M_TheRB.velocity.magnitude;
    }
    private void RotatingToRight()
    { 
        // the rotating to te m_rotation degree if you take a right turn
        float _time = Time.deltaTime * m_lerptime;
        Quaternion _Rrot = m_RacerObject.transform.localRotation;
        m_RacerObject.transform.localRotation = Quaternion.Lerp(_Rrot,Quaternion.Euler(_Rrot.x, _Rrot.y ,-m_Rotation), _time);
    }

    private void RotatingToNormal()
    { 
        // to what the rotation the gree has to bee if je dont turn
        float _time = Time.deltaTime * m_lerptime;
        Quaternion _Nrot = m_RacerObject.transform.localRotation;
        m_RacerObject.transform.localRotation = Quaternion.Lerp(_Nrot, Quaternion.Euler(_Nrot.x, _Nrot.y, m_Normalrotation), _time);
    }

    private void RotatingToLeft()
    {
        // go rotating to the m_rotation degree if you take a left turn
        float _time = Time.deltaTime * m_lerptime;
        Quaternion _Lrot = m_RacerObject.transform.localRotation;
        m_RacerObject.transform.localRotation = Quaternion.Lerp(_Lrot, Quaternion.Euler(_Lrot.x, _Lrot.y, m_Rotation), _time);
    }
}

