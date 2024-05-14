using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "turtleshield" || collision.transform.tag == "Enemy") {
            Debug.LogError("canonhit");
            Transform trtl = collision.transform.parent;
            turtle t = trtl.transform.GetComponentInChildren<turtle>();
            
            t.TakeDamage(150);
            Debug.LogError(this.name);
            GameObject player = GameObject.Find("Player");
            Drive dr = player.GetComponent<Drive>();
            dr.Canonballtxt();
            Destroy(this);
            
        }
        if (collision.transform.tag == "AbmEnemy")
        {
            aboleth_mb abol = collision.transform.GetComponent<aboleth_mb>();
            abol.health -= 150;
            GameObject player = GameObject.Find("Player");
            Drive dr = player.GetComponent<Drive>();
            dr.Canonballtxt();
            Destroy(gameObject);
        }
    }
}
