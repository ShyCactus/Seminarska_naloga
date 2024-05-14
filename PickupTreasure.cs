using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupTreasure : MonoBehaviour
{
    public  static  int money = 0;
    public TMP_Text mt;
    private void Awake()
    {
        money = (int)coinkeep.coins;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Treasure") {
            Treasure t = other.GetComponent<Treasure>();
            money += t.Amount;
            coinkeep.totalcoins += t.Amount;
            mt.text = money.ToString();
            Destroy(other.gameObject);
        }
    }
}
