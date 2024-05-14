using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levl0tips : MonoBehaviour
{
    public door d;
    public door d1;
    public void Hide() {

        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.SetActive(false);
    
    }
    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.None;
        d.time = 0;
        d1.time = 0;
    }
}
