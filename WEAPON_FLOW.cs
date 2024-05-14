using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WEAPON_FLOW : MonoBehaviour
{
    public Transform followSphere;

    public Transform CamTransform;

    void Update()
    {
        
        if(CamTransform.eulerAngles.x < 180)
        transform.localEulerAngles = new Vector3(CamTransform.localEulerAngles.x / 2, transform.localEulerAngles.y, transform.localEulerAngles.z);
        else
            transform.localEulerAngles = new Vector3((-360 +CamTransform.localEulerAngles.x  )/2, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}