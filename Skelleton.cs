using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelleton : MonoBehaviour
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
    public float shealth = 75;
    public bool canDamage;
    public AudioSource hit;

    public Animator a;

    public bool jumponplyr;
    public float timetilldamage;
    public float rbmag;
    public bool canTakedamage;
    public ParticleSystem ps;

    public float searchtimer;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        shealth = health;
        canTakedamage = true;
    }
    private void FixedUpdate()
    {
        timetilldamage -= Time.deltaTime;
        rbmag = rb.velocity.magnitude;
        vdiff = player.position.y - transform.position.y;
        timetilljump -= Time.deltaTime;
        dir = player.transform.position - transform.position;
        distancetoPlayer = Mathf.Sqrt((transform.position.x - player.position.x) * (transform.position.x - player.position.x) + (transform.position.z - player.position.z) * (transform.position.z - player.position.z));
        if (SearchingForPlayer)
        {
            if ((!(distancetoPlayer < 1.5f) || (timetilldamage > 0)) && canTakedamage)
            {
                a.SetBool("Attack", false);
                a.SetBool("Walk", true);
                a.SetBool("Rebirth", false);
                a.SetBool("Die", false);
                a.SetBool("Damage", false);
                searchtimer += Time.deltaTime;
            }
            

            //transform.LookAt(player);
            if(canTakedamage)
            transform.forward = Vector3.Lerp(transform.forward, dir, rotspeed * Time.deltaTime);


            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            if(canTakedamage)
            rb.AddForce(transform.forward * speed);



            if (vdiff > .9f && timetilljump < 0 && canTakedamage)
            {

                rb.AddForce(transform.up * jumpforce * (vdiff + .5f));
                timetilljump = 1;

            }




            if (transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            }


        }
        if (searchtimer > 15) {

            transform.position = new Vector3(player.position.x, player.position.y + .5f, player.position.z);
            searchtimer = 0;
            ps.Play();
        }

        if (distancetoPlayer < 1.5f && canTakedamage && timetilldamage <0) {
            
            timetilldamage = 2.5f;
            hit.Play();
            if(Mathf.Abs(vdiff) < 1)
            searchtimer = 0;
            StartCoroutine("Dam");
            a.SetBool("Attack", true);
            a.SetBool("Walk", false);
            a.SetBool("Rebirth", false);
            a.SetBool("Die", false);
            a.SetBool("Damage", false);

        }

        if (jumponplyr)
        {
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
        if (health <= 0)
        {
            a.SetBool("Attack", false);
            a.SetBool("Walk", false);
            a.SetBool("Rebirth", false);
            a.SetBool("Die", true);
            a.SetBool("Damage", false);
            StartCoroutine("Rebirth");
        }

    }
    public void TakeDamage(float damage) {
        if (canTakedamage) {
            a.SetBool("Attack", false);
            a.SetBool("Walk", false);
            a.SetBool("Rebirth", false);
            a.SetBool("Die", false);
            a.SetBool("Damage", true);
            health -= damage;
        }
        
    }
    IEnumerator Rebirth()
    {
        canTakedamage = false;
        yield return new WaitForSeconds(.3f);
        health = shealth;

        yield return new WaitForSeconds(20);
        a.SetBool("Attack", false);
        a.SetBool("Walk", false);
        a.SetBool("Rebirth", true);
        a.SetBool("Die", false);
        a.SetBool("Damage", false);

        yield return new WaitForSeconds(.3f);
        canTakedamage = true;
        yield return new WaitForSeconds(.1f);
        a.SetBool("Attack", false);
        a.SetBool("Walk", true);
        a.SetBool("Rebirth", false);
        a.SetBool("Die", false);
        
        a.SetBool("Damage", false);


    }
    IEnumerator Dam() {
        yield return new WaitForSeconds(.6f);
        
        canDamage = true;
        yield return new WaitForSeconds(.2f);
        canDamage = false;
        
    
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
