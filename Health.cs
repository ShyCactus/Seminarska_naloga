using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float HealthAmount = 100;
    public bool InPoison;
    float poisonDamage = 5;
    public Slider HealthBar;

     void Start()
    {
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (InPoison) {
            HealthAmount -= poisonDamage * Time.fixedDeltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Poison") {

            InPoison = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Poison")
        {

            InPoison = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Poison")
        {

            InPoison = false;
        }
    }
}
