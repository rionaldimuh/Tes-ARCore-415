using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class ARcoreDevice : MonoBehaviour
{
    [SerializeField] Text arcoreText;
    ARSession m_Session;
    private void Awake()
    {
        m_Session = FindObjectOfType<ARSession>();
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
            arcoreText.gameObject.SetActive(true);
        }
        else
        {
            // Start the AR session
            m_Session.enabled = true;
            arcoreText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
