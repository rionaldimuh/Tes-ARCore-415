using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHand : MonoBehaviour
{
    [SerializeField] 
    private float RotateSpeed = 2f, Radius = 50f;

    private Vector2 centre;
    private float angle;

    private void Start()
    {
        centre = transform.position;
    }

    private void Update()
    {
        angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        transform.position = centre + offset;
    }
}
