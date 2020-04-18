﻿using System;
using UnityEngine;
using System.Collections;
 
public class CameraFacingBillboard : MonoBehaviour
{
    private Camera m_Camera;
 
    //Orient the camera after all movement is completed this frame to avoid jittering
    private void Start()
    {
        m_Camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}