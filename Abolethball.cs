using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abolethball : MonoBehaviour
{
    public Transform target;
    public float effectstrength;
    public Rigidbody rb;
    
    void Awake()
    {
         int i = Random.Range(0, 5);
        if(i == 0) 
        target = GameObject.Find("abballtar").transform;
        if (i == 1)
            target = GameObject.Find("abballtar (1)").transform;
        if (i == 2)
            target = GameObject.Find("abballtar (2)").transform;
        if (i == 3)
            target = GameObject.Find("abballtar (3)").transform;
        if (i == 4)
            target = GameObject.Find("abballtar (4)").transform;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name != "aboleth_mb")
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.LookAt(target);
        rb.AddForce(transform.forward * effectstrength);
    }
}
