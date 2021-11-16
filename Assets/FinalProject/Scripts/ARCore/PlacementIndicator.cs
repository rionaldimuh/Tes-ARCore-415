using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private ARManager ARManager;
    private GameObject marker;
    [SerializeField] GameObject handCanvas;
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
        handCanvas.SetActive(false);
    }

    private void Update()
    {
        MarkerMove();
    }

    void MarkerMove()
    {
        if (ARManager.SDK == ARManager.AR.XR8thWall) //Non ARCore
        {
            RaycastHit hit;
            Ray ray = XRCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
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
            var ray = new Vector2(Screen.width / 2, Screen.height / 2);
            rayManager.Raycast(ray, hits, TrackableType.Planes);
            
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
                marker.SetActive(false);
            }
        }

        if (isSurfaceReady)
        {
            handCanvas.SetActive(false);
        }
        else
        {
            handCanvas.SetActive(true);
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
