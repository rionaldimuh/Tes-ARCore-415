using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnerrr : MonoBehaviour
{
    [SerializeField] GameObject prefabObject;
    Indikator indikator;
    [SerializeField] GameObject buttonSpawn, buttonUnspawn;
    public bool spawned;
    void Start()
    {
        indikator = FindObjectOfType<Indikator>();
        buttonUnspawn.SetActive(false);
        prefabObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned)
        {
            buttonSpawn.SetActive(false);
            buttonUnspawn.SetActive(true);
        }
        else
        {
            buttonSpawn.SetActive(true);
            buttonUnspawn.SetActive(false);
        }
    }

    public void SpawnObject()
    {
        if (indikator.isSurfaceReady)
        {
            //GameObject obj = Instantiate(prefabObject, indikator.transform.position, indikator.transform.rotation);
            prefabObject.SetActive(true);
            prefabObject.transform.position = indikator.transform.position;
            spawned = true;
        }
    }

    public void UnSpawnObject()
    {
        prefabObject.SetActive(false);
        spawned = false;
    }
}
