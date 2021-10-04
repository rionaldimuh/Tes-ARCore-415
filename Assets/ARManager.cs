﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARManager : MonoBehaviour
{
    [SerializeField]ARSession m_Session;

    [SerializeField]GameObject XR8thWall, ARSessionOrigin;
    public bool isARF;
    [SerializeField] Text debug;
    private void Awake()
    {
        StartCoroutine(Start());
    }

    IEnumerator Start()
    {
        if ((ARSession.state == ARSessionState.None) ||
           (ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            // Start some fallback experience for unsupported devices
            m_Session.enabled = false;
            //ARSessionOrigin.SetActive(false);
            XR8thWall.SetActive(true);
            isARF = false;
        }
        else
        {
            // Start the AR session
            m_Session.enabled = true;
           // ARSessionOrigin.SetActive(true);
            XR8thWall.SetActive(false);
            isARF = true;
        }
        debug.text = "ARCore : " + ARSession.state.ToString() + "\nXRController: " + XR8thWall.activeSelf;
    }

}
