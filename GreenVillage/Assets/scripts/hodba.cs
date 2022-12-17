using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hodba : MonoBehaviour
{
public int speed=10;

    public int runspeed = 30;
    public Rigidbody rb;
    public int jumpPower=250;
    public bool ground = true;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }


    void Update()
    {
       if(Input.GetKey(KeyCode.W)){
            rb.AddForce(transform.forward*speed);
            if(Input.GetKey(KeyCode.LeftShift)){
                rb.AddForce(transform.forward * speed);
            }
                 } 
       if(Input.GetKey(KeyCode.S)){
            rb.AddForce(-transform.forward * speed);
          if(Input.GetKey(KeyCode.LeftShift)){
                rb.AddForce(-transform.forward * speed);
            }
          }
        
       if(Input.GetKey(KeyCode.D)){
            rb.AddForce(transform.right * speed);
        } 
    if(Input.GetKey(KeyCode.A)){
            rb.AddForce(-transform.right * speed);
    } 
      if(Input.GetKeyDown(KeyCode.Space)){
        if(ground){
          rb.AddForce(transform.up*jumpPower);
        }
       } 
    }
}



    
   