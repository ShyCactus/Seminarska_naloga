using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatrotfix : MonoBehaviour
{
    private Quaternion rot;
    private void FixedUpdate()
    {
        rot.eulerAngles = new Vector3(Mathf.Clamp(rot.eulerAngles.x, -90, 90), rot.eulerAngles.y, Mathf.Clamp(rot.eulerAngles.z, -90, 90));
        transform.rotation = rot;
    }
}
