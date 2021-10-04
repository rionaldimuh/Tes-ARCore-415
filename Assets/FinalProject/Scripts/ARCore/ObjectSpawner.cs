using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    GameObject myObj, myParent;
    private PlacementIndicator placementIndicator;
    ARManager arManager;
    [SerializeField]Text spawnCount;
    
    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        arManager = FindObjectOfType<ARManager>();
        myParent = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCount.text = "Lingkaran: " + placementIndicator.transform.GetChild(0).gameObject.activeSelf + "\nObject Count: " + myParent.transform.childCount
                            + "\nSurface: " + placementIndicator.isSurfaceReady;
        //if (Input.touchCount == 0 && Input.touches[0].phase == TouchPhase.Began)
        //{

        //}
        // Tap to place
        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{

        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Surface")))
        //    {
        //        CreateObject(placementIndicator.transform.position);
        //        //CreateObject(new Vector3(hit.point.x, hit.point.y + heightAdjustment, hit.point.z));

        //    }
        //    else
        //    {
        //        CreateObject(placementIndicator.transform.position);
        //        //// It tap doesn't hit surface, place in "air" at touch point in front of camera at a distance of distanceFromCamera
        //        //Vector3 touchPos = Input.GetTouch(0).position;
        //        //touchPos.y = touchPos.y + heightAdjustment;
        //        //touchPos.z = distanceFromCamera;

        //        //Vector3 objPos = Camera.main.ScreenToWorldPoint(touchPos);

        //    }
        //}

    }

    public void SpawnObject()
    {
        if (placementIndicator.transform.GetChild(0).gameObject.activeSelf)
        {
            GameObject obj = Instantiate(objectToSpawn, placementIndicator.transform.position,
                    placementIndicator.transform.rotation);
            obj.transform.parent = myParent.transform;
            if (!arManager.isARF)
            {
                //if (objectToSpawn)
                //{
                //    myObj = GameObject.Instantiate(objectToSpawn);
                //}
                //else
                //{
                //    myObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //}
                //myObj.transform.parent = myParent.transform;
                //myObj.transform.position = placementIndicator.transform.GetChild(0).transform.position;
                //myObj.transform.localScale = new Vector3(5, 5, 5);
                obj.transform.localScale = new Vector3(5, 5, 5);
            }
            //else
            // {
            //     GameObject obj = Instantiate(objectToSpawn, placementIndicator.transform.position,
            //             placementIndicator.transform.rotation);
            // }
        }

    }

}
