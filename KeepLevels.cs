using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLevels : MonoBehaviour
{
    public static int level = 1;
    public int l;
    private void Awake()
    {
       
        DontDestroyOnLoad(this.gameObject);
    }
    private void FixedUpdate()
    {
        l = level;
    }
}
