using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class turtle : MonoBehaviour
{
    public Transform canonball;
    public Transform shootpos;
    public float shootStrength = 100;
    public Transform cannon;
    public GameObject Player;
    public GameObject[] targets;
    public GameObject Target;


    public Transform _turtle;
    float timetilltargetchange = 2;
    public bool shooting = true;
    public bool hiding = true;
    public bool moving = false;
    public float hideSpeed;
    public float moveSpeed;
    public float lookSpeed;
    int Move3shots = 0;
    public Vector3 TargetMovePos;
    public int randx;
    public int randz;
    public Rigidbody rb;
    public float rotdelay;
    public float ivolunarability;

    public Slider HB;
    public float Health;
   

    public float shootTimer;
    public AudioSource scream;
   
    void Start()
    {
        Player =  GameObject.Find("Player");
        TargetMovePos = _turtle.position;
       
        StartCoroutine("shoot");
    }
    private void Awake()
    {
        Drive.canEnterNextLevel = false;
    }

    private void FixedUpdate()
    {
        ivolunarability -= Time.deltaTime;
        
        if (shooting)
        {
            timetilltargetchange -= Time.deltaTime;
            if (timetilltargetchange < 0)
            {
                timetilltargetchange = 2;
                int selected = Random.Range(0, targets.Length);
                Target = targets[selected];

            }
            if (_turtle.position.y < -1) {

                _turtle.position = new Vector3(_turtle.position.x, _turtle.position.y + 0.36f, _turtle.position.z);
            }
            var TargetRot = Quaternion.LookRotation(targets[1].transform.position - _turtle.position);
            TargetRot.eulerAngles = new Vector3(TargetRot.eulerAngles.x, TargetRot.eulerAngles.y - 90, TargetRot.eulerAngles.z);
            _turtle.rotation = Quaternion.Slerp(_turtle.rotation, TargetRot, lookSpeed / 5 * Time.deltaTime);

            cannon.LookAt(Target.transform);
            cannon.Rotate(new Vector3(0, -90, 0));
        }
        if (hiding && (_turtle.position.y > -20))
        {
            _turtle.position = Vector3.Lerp(new Vector3(_turtle.position.x, _turtle.position.y, _turtle.position.z), new Vector3(_turtle.position.x, -22, _turtle.position.z),hideSpeed * Time.deltaTime);

            
                _turtle.eulerAngles = new Vector3(_turtle.eulerAngles.x, _turtle.eulerAngles.y, _turtle.eulerAngles.z - .3f);
            
            

        }
        else if (hiding) {
            moving = true;
            hiding = false;
            randx = 0;
            randz = 0;
            while (randx == 0 || randz == 0)
            {
                randx = Random.Range(-1, 2) ;
                randz = Random.Range(-1, 2) ;
            }

            while(Mathf.Abs(_turtle.position.x - TargetMovePos.x) < 8 && Mathf.Abs(_turtle.position.z - TargetMovePos.z) < 8)
            TargetMovePos = new Vector3(targets[Random.Range(0, targets.Length)].transform.position.x + Random.Range(10, 45)*randx ,0, targets[Random.Range(0, targets.Length)].transform.position.z + Random.Range(10, 45)*randz);
            /*
            var TargetRot = Quaternion.LookRotation(TargetMovePos - _turtle.position);
            TargetRot.eulerAngles = new Vector3(TargetRot.x, TargetRot.y - 90, TargetRot.z);
            _turtle.rotation = Quaternion.Slerp(_turtle.rotation, TargetRot, lookSpeed * Time.deltaTime);
            */

        }
        if (moving) {

            _turtle.position = Vector3.Lerp(_turtle.position, TargetMovePos, moveSpeed* ( Mathf.Abs(TargetMovePos.x + TargetMovePos.z - _turtle.position.x - _turtle.position.z) + 20 ) * Time.deltaTime);
            Vector3 rtv = Vector3.RotateTowards(_turtle.forward,TargetMovePos, lookSpeed * Time.deltaTime, 3.0F);
            _turtle.rotation = Quaternion.Slerp(_turtle.rotation, Quaternion.LookRotation(rtv), Time.deltaTime);

            // _turtle.eulerAngles = new Vector3(_turtle.eulerAngles.x, _turtle.eulerAngles.y, _turtle.eulerAngles.z + .1f);

           // _turtle.LookAt(TargetMovePos);
            //_turtle.Rotate(new Vector3(0, -90, 0));

            if ((Mathf.Abs(_turtle.position.x - TargetMovePos.x) < 6) && (Mathf.Abs(_turtle.position.z - TargetMovePos.z) < 6)) {

                moving = false;
                shooting = true;
               
                Move3shots = 0;
                StartCoroutine("shoot");
                
            }
        }
        if (hiding) { shooting = false; }
        if (moving) { hiding = false; }
        if (shooting) { moving = false;  }
    }


    public void TakeDamage( float damage) {

        if (ivolunarability < 0)
        {
            ivolunarability = .1f;
            Health -= damage;
            HB.value = Health;
            if (Health < 1)
            {
                Drive.canEnterNextLevel = true;
                StartCoroutine("Die");
            }

            shooting = false;
            moving = false;
            hiding = true;
        }
    }

    IEnumerator Die() {
        shooting = false;
        moving = false;
        hiding = true;
        scream.Play();
        yield return new WaitForSeconds(2);
        Destroy(_turtle.gameObject);

    }
    IEnumerator shoot() {
        
        yield return new WaitForSeconds(5.5f);

        Move3shots++;
        if (shooting) {
            GameObject _canonBall = GameObject.Instantiate(canonball.gameObject, shootpos.position, shootpos.rotation);
            Rigidbody rb = _canonBall.GetComponent<Rigidbody>();
            rb.AddForce(_canonBall.transform.forward * shootStrength);

            StartCoroutine("shoot");

        }
        if (Move3shots > 1) {

            hiding = true;
            shooting = false;
            Move3shots = 0;
        }
        



    }
}
