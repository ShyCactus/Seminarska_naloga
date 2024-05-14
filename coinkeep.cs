using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinkeep : MonoBehaviour
{
    public static float coins;
    public static float totalcoins;
    public  float coinst;
    public  float totalcoinst;
    void Awake() {
        DontDestroyOnLoad(gameObject);
        
    }
    private void FixedUpdate()
    {
        coinst = coins;
        totalcoinst = totalcoins;
    }

}
