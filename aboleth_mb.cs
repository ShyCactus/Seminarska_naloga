using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class aboleth_mb : MonoBehaviour
{
    public float firerate = 5;
    private float sfirerate;
    public float health = 1000;
    public GameObject [] projectiles;
    public Transform player;
    public Slider hb;
  public int  randprojc;
    public Transform Ship;
    public float mspeed;
    public Animator anim;
    public AudioSource mr;
    public GameObject Scream;

    private void Awake()
    {
        Drive.canEnterNextLevel = false;
    }
    private void Start()
    {
        sfirerate = firerate;
    }
    private void FixedUpdate()
    {
        firerate -= Time.deltaTime;
        transform.LookAt(player);
        anim.SetBool("Shoot", true);
        hb.value = health;
        if (firerate < 0) {
            anim.SetBool("Shoot", true);
            firerate = sfirerate;
             randprojc = Random.Range(0, projectiles.Length);
            GameObject projectile = GameObject.Instantiate(projectiles[randprojc], new Vector3(transform.position.x, transform.position.y+10, transform.position.z), transform.rotation);
            Rigidbody prb = projectile.GetComponent<Rigidbody>();
            if (prb.name != "Abball(Clone)")
                prb.AddForce(projectile.transform.forward * 35, ForceMode.Impulse);
            else {
                prb.AddForce(projectile.transform.forward *12 , ForceMode.Impulse);
            }

        }
        if (health <= 0) {
            Drive.canEnterNextLevel = true;
            StartCoroutine("Destroy");
            transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
            

        }
        transform.RotateAround(Ship.position, new Vector3(0,360,0), mspeed * Time.deltaTime);
    }
    IEnumerator Destroy() {
        mr.Play();
        Scream.SetActive(true);
        yield return new WaitForSeconds(4);
        Scream.SetActive(false);
        Destroy(gameObject);
    }

}
