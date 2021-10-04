using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject marker;
    private ARManager ARManager;
    [SerializeField]private Camera XRCamera;
    public bool isARCoreReady = false, isSurfaceReady;

    private void Awake()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        ARManager = FindObjectOfType<ARManager>();
    }
    private void Start()
    {

        marker = transform.GetChild(0).gameObject;
        if (!ARManager.isARF)//Tidak support ARCore
        {
            marker.transform.localScale = Vector3.one;
            marker.SetActive(true);
        }
        else
        {
            marker.SetActive(false);
            //StartCoroutine(InitARCoreMarker());
        }
    }

    private void Update()
    {
        if (!ARManager.isARF)//Tidak support ARCore
        {
            // Start some fallback experience for unsupported devices
            MarkerMove();
        }
        else //Device suppport
        {
            //if (isARCoreReady)
            //{
                //shoot a raycast fromt he center of the screen
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

                //if we hit an AR plane, update the position and rotation
                if (hits.Count > 0)
                {
                    transform.position = hits[0].pose.position;
                    transform.rotation = hits[0].pose.rotation;
                    if (!marker.activeInHierarchy)
                        marker.SetActive(true);
                    isSurfaceReady = true;
                }
                else
                {
                    isSurfaceReady = false;
                }
            //}
            //else
            //    marker.SetActive(false);
        }
    }

    void MarkerMove()
    {
        marker.transform.position = XRCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, DistanceFromCamera()));
    }

    public float DistanceFromCamera()
    {
        return 90 - XRCamera.transform.eulerAngles.x;
    }

    //IEnumerator InitARCoreMarker()
    //{
    //    yield return new WaitForSeconds(1);
    //    isARCoreReady = true;
    //}
}
