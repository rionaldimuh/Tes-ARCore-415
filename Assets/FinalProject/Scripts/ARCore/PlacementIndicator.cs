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
            //marker.transform.localScale = Vector3.one;
            marker.SetActive(false);
        }
        else
        {
            marker.SetActive(false);
            //StartCoroutine(InitARCoreMarker());
        }
    }

    private void Update()
    {
        MarkerMove();
    }

    void MarkerMove()
    {
        if (!ARManager.isARF) //Non ARCore
        {
            //marker.transform.position = XRCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, DistanceFromCamera()));
            RaycastHit hit;
            Ray ray = XRCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            // The "Surface" GameObject with an XRSurfaceController attached should be on layer "Surface"
            // If tap hits surface, place object on surface
            if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Surface")))
            {
                transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                marker.SetActive(true);
                isSurfaceReady = true;
            }
            else
            {
                isSurfaceReady = false;
                marker.SetActive(false);
            }
        }
        else //ARCore
        {
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
        }
    }
    
    //public float DistanceFromCamera()
    //{
    //    return 90 - XRCamera.transform.eulerAngles.x;
    //}

    //IEnumerator InitARCoreMarker()
    //{
    //    yield return new WaitForSeconds(1);
    //    isARCoreReady = true;
    //}
}
