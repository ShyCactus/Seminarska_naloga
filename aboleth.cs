using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aboleth : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    public float speed = 1;
    public bool inWater;
    public bool SearchingForPlayer;
    
    public float distancetoPlayer;
    public float timetilljump;
    public float jumpforce;
    public float jumpforce2;
    public Vector3 dir;
    public float rotspeed;
    public float vdiff;
    public float health = 75;

    
   
    public bool jumponplyr;

    public float rbmag;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
       
    }
    private void FixedUpdate()
    {
        rbmag = rb.velocity.magnitude;
        vdiff = player.position.y - transform.position.y;
        timetilljump -= Time.deltaTime;
        dir = player.transform.position - transform.position;
        distancetoPlayer = Mathf.Sqrt((transform.position.x - player.position.x)* (transform.position.x - player.position.x) + (transform.position.z - player.position.z)* (transform.position.z - player.position.z));
        if (SearchingForPlayer)
        {
            
            //transform.LookAt(player);
            transform.forward = Vector3.Lerp(transform.forward, dir, rotspeed * Time.deltaTime );


            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            rb.AddForce(transform.forward * speed);

           
               
                if (vdiff > .9f && timetilljump < 0)
                {

                    rb.AddForce(transform.up * jumpforce * (vdiff + .5f));
                    timetilljump = 1;

                }
           

                
            
            if (transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                
            }

           
        }
        
       
        if (jumponplyr) {
            rb.AddForce(transform.forward * jumpforce2);
            transform.LookAt(player);
            rb.velocity = (Vector2)Vector3.ClampMagnitude((Vector3)rb.velocity, 5);
            if (Mathf.Abs(vdiff) < 3 && distancetoPlayer < 6)
            {
                SearchingForPlayer = true;
                inWater = false;
                jumponplyr = false;
                

            }

        }
        if (health <= 0) {

            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sea")
        {
            
            SearchingForPlayer = false;
            inWater = false;
            jumponplyr = true;
            
            
            
        }
    }
}
