using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpandDown : MonoBehaviour
{

    [SerializeField] private float waveLength;
    [SerializeField] private float amplitude;
    [SerializeField] private float waveSpeed;

    private Vector3 originalPos;

    private float theta = 0;

    private void Start()
    {
        waveSpeed = waveSpeed / 32f;
        originalPos = transform.position;
    }





    private void FixedUpdate()
    {
        theta += waveSpeed;

        // This is by far the shit
        transform.Translate(Vector3.down * (Mathf.Sin(theta * waveLength) * amplitude));  
    }

}
