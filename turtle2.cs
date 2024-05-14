using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtle2 : MonoBehaviour
{
    public Rigidbody rb;
    public Transform turtle;
    public int State;
    public float hidingSpeed;
   
    public float moveSpeed;
    public float ShootStrength;
    public Transform Ship;
    public Vector3 TargetPos;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //hiding





        turtle.forward = rb.velocity;

        if (State == 1) {
            rb.AddForce(Vector3.down * hidingSpeed, ForceMode.Force);
            if (turtle.position.y < -12) {
                
                TargetPos = new Vector3(Ship.position.x + Random.Range(10, 45), 0, Ship.position.z + Random.Range(10, 45));
                State = 2;
            }
            
        }if (State == 2) {

            rb.AddForce((TargetPos - turtle.position).normalized * moveSpeed, ForceMode.Force);
            if ((TargetPos.x - turtle.position.x < 6) && ((TargetPos.z - turtle.position.z < 6))) {

                State = 3;
            }

        }if (State == 3) { rb.velocity = Vector3.zero; }
    }
}
