using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovment : MonoBehaviour
{
    [SerializeField]
    private Transform m_GroundRayPoint;
    [SerializeField]
    private LayerMask m_WhatGround;
    [SerializeField]
    private float m_GroundRayLenght;
    private Camera m_camera;



    private void FixedUpdate()
    {
        // dit word niet gebruikt
        RaycastHit _Hit;

        if (Physics.Raycast(m_GroundRayPoint.position, -Vector3.up, out _Hit, m_GroundRayLenght, m_WhatGround))
        {
            float _time = Time.deltaTime * 10f;
            // schoots a ray cat that checks what the rotaiotn is of the object and chengses to the rotation
            Quaternion _target = Quaternion.FromToRotation(Vector3.up, _Hit.normal) * transform.localRotation;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _target, _time);
            Vector3 eulerRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(eulerRotation.x, 0, eulerRotation.z);

        }
    }
}
