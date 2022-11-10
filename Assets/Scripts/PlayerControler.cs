using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerControler : MonoBehaviour
{

    Rigidbody rb;
    Animator animCtrl;
    public float MaxHP;
    public float MaxSpeed;
    public float acceleration = 30;
    public float drag = 30;
    public float RotateSpeed = 10;

    float hp;
    float speed;
    float goRight;
    float goLeft;
    bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animCtrl = GetComponent<Animator>();

        animCtrl.SetFloat("Speed", 0);
        animCtrl.SetFloat("HP", 1);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !isDead)
        {
            Accelerate();
        }
        else
        {
            Decelarate();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
        else
            transform.Rotate(0, 0, 0);


        if (Input.GetKey(KeyCode.RightArrow))      
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);        
        else
            transform.Rotate(0, 0, 0);




        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //        rb.angularDrag = 0.8F;
        //    else
        //        rb.angularDrag = 0;

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    rb.angularDrag = 0.8F;
        //else
        //    rb.angularDrag = 0;

        //if the HP get to 0 => Dead
        if (Input.GetKey(KeyCode.Q) && !isDead)
        {
            animCtrl.SetTrigger("Dead");
            isDead = true;
        }

        else if (Input.GetKey(KeyCode.M) && isDead == true)
            animCtrl.ResetTrigger("Dead");
    }

    public void Accelerate()
    {
        if (rb.velocity.magnitude < MaxSpeed)
        {
            //animCtrl.SetFloat("Speed", currentSpeed / MaxSpeed);
            rb.AddRelativeForce(Vector3.forward * acceleration);
            animCtrl.SetFloat("Speed", Mathf.Clamp01(rb.velocity.magnitude / MaxSpeed));
        }
    }

    private void Decelarate()
    {
        //rb.AddRelativeForce(Vector3.forward * currentSpeed);

        Vector3 v = transform.InverseTransformVector(rb.velocity);
        if (v.z > 0)
        {
            //rb.AddRelativeForce(Vector3.back * acceleration);
            rb.velocity = new Vector3(0, 0, 0);
            animCtrl.SetFloat("Speed", Mathf.Clamp01(rb.velocity.magnitude / MaxSpeed));
        }
    }


}
