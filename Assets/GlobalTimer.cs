using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalTimer : MonoBehaviour
{
    public Transform minuteHand;
    public Transform hourHand;
    [Range(0f,500f)]
    public float speed = 5f;


    // Update is called once per frame
    void Update()
    {
        minuteHand.Rotate(Vector3.forward*-speed*Time.deltaTime);
        hourHand.Rotate(Vector3.forward*(-speed/12f)*Time.deltaTime);
        
    }
}
