using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveBoat : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float rotspeed;
    public Drive d;
    public Rigidbody rb;
    public Transform Sail;
    public float ClampSpeed;
    public float mgn;
    public GameObject hole;
    public Transform[] hits;
    public float[] holes = new float[30];
    public float waterAmount;
    float shootprot;
    public float holecount;
    public Transform water;
    public float pba;
    public static bool waterCanChange;
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        rb.AddForce(transform.forward*speed, ForceMode.Force);
        transform.Rotate(0, d.wheelrot * rotspeed, 0, Space.Self);
        Sail.localScale = new Vector3(speed / 140 ,Sail.localScale.y, Sail.localScale.z);
        mgn = rb.velocity.magnitude;
        if (rb.velocity.magnitude > ClampSpeed) {

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, ClampSpeed);
        
        }
        shootprot -= Time.deltaTime;
        if (waterCanChange)
        {
            waterAmount += holecount / 2 * Time.deltaTime;
        }
        if (holecount == 0 && waterAmount >0 && waterCanChange) {

            waterAmount -= pba * Time.deltaTime;
        
        }

        water.transform.localPosition = new Vector3(water.transform.localPosition.x,-1.3f + waterAmount /50 , water.transform.localPosition.z);

        if (waterAmount > 100) {
            KeepLevels.level = d.level-1;
            SceneManager.LoadScene(5);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (shootprot < 0) { 
            if (collision.transform.tag == "cball") {
                holecount++;
            float shx = 100;
            float shz = 100;
            int selectedHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                float x = Mathf.Abs(collision.transform.position.x - hits[i].position.x);
                float z = Mathf.Abs(collision.transform.position.z - hits[i].position.z);

                if ((x + z) < (shx + shz)) {
                    shx = x;
                    shz = z;
                    selectedHit = i;
                }


            }


            GameObject _hole = GameObject.Instantiate(hole, hits[selectedHit].position, hits[selectedHit].rotation);
            _hole.gameObject.SetActive(true);
            _hole.transform.parent = transform;

                for (int k = 0; k < holes.Length; k++)
                {

                    if ((_hole.transform.localPosition.x +.02 > holes[k] )&& (_hole.transform.localPosition.x - .02 < holes[k])) {
                        _hole.transform.position = new Vector3(_hole.transform.position.x , hole.transform.position.y + Random.Range(.1f, .3f), hole.transform.position.z );
                    }
                    if (holes[k] == 0) {
                        holes[k] = _hole.transform.localPosition.x;
                        break;
                    }
                }
                Destroy(collision.gameObject);
            }
            if (collision.transform.tag == "Rock") {


                holecount++;
                    
                    float shx = 100;
                    float shz = 100;
                    int selectedHit = 0;
                    for (int i = 0; i < hits.Length; i++)
                    {
                        float x = Mathf.Abs(collision.transform.position.x - hits[i].position.x);
                        float z = Mathf.Abs(collision.transform.position.z - hits[i].position.z);

                        if ((x + z) < (shx + shz))
                        {
                            shx = x;
                            shz = z;
                            selectedHit = i;
                        }


                    }


                    GameObject _hole = GameObject.Instantiate(hole, hits[selectedHit].position, hits[selectedHit].rotation);
                    _hole.gameObject.SetActive(true);
                    _hole.transform.parent = transform;

                    for (int k = 0; k < holes.Length; k++)
                    {

                        if ((_hole.transform.localPosition.x + .02 > holes[k]) && (_hole.transform.localPosition.x - .02 < holes[k]))
                        {
                            _hole.transform.position = new Vector3(_hole.transform.position.x, hole.transform.position.y + Random.Range(.1f, .3f), hole.transform.position.z);
                        }
                        if (holes[k] == 0)
                        {
                            holes[k] = _hole.transform.localPosition.x;
                            break;
                        }
                    }

                Destroy(collision.gameObject);

                



            }
            shootprot = 1;

            
            
            
            
        }
    }
}