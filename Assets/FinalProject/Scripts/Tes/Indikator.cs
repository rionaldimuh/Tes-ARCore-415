using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Indikator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject marker; //lingkaran
    [SerializeField] GameObject handCanvas;
    //[SerializeField] private Camera XRCamera; //camera buat xr 8thWall
    [HideInInspector]public bool isSurfaceReady;
    [SerializeField]Spawnerrr spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawnerrr>();
        rayManager = FindObjectOfType<ARRaycastManager>(); //FindObjectOfType buat nyari GameObjek yang punya script <ARRaycastManager>
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
        //shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (!spawner.spawned)
        {
            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                if (!marker.activeInHierarchy)
                    marker.SetActive(true);
                isSurfaceReady = true;
                handCanvas.SetActive(false);
            }
            else
            {
                isSurfaceReady = false;
                marker.SetActive(false);
                handCanvas.SetActive(true);
            }
        }
        //if we hit an AR plane, update the position and rotation
        

        //if (isSurfaceReady)
        //{
        //    handCanvas.SetActive(false);
        //}
        //else
        //{
        //    handCanvas.SetActive(true);
        //}
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
