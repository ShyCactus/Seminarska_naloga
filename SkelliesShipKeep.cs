using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelliesShipKeep : MonoBehaviour
{
    public Transform boat;

    private void FixedUpdate()
    {
        this.transform.position = boat.position;
        this.transform.eulerAngles = boat.eulerAngles;
    }
}
