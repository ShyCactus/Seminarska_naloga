using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    public Transform camera;
    public Drive dr;
    public FirstPersonMovement fpm;
    public Crouch cr;
    public Jump jmp;
    public Rigidbody rb;
    public float swimspeed = 10;
    private void FixedUpdate()
    {
        if (!dr.onBoat && dr.swimming)
        {
            rb.useGravity = false;
            fpm.enabled = false;
            cr.enabled = false;
            jmp.enabled = false;


            //swim
            if (Input.GetKey(KeyCode.W)) {
                rb.AddForce(camera.forward * swimspeed);        
                
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-camera.forward * swimspeed/2);

            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(camera.right * swimspeed/2);

            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-camera.right * swimspeed/2);

            }
        }
        else if(!fpm.isActiveAndEnabled){
            fpm.enabled = true;
            cr.enabled = true;
            jmp.enabled = true;
            rb.useGravity = true;
        }
    }

}
