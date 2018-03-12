using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Quaternion targetRotation;
    public Rigidbody rb;
    public float speed = 100f;
    //public float turnspeed = 0.1f;
    public float rotationVelocity = 100f;

    // Use this for initialization
    void Start()
    {
        targetRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //MoveForward
        //transform.position += Input.GetAxis("Vertical") * Vector3.forward  * speed;
        //Rotate
        //transform.Rotate(Vector3.right * Input.GetAxis("Horizontal") * turnspeed* Time.deltaTime);
        if (Input.GetAxis("Horizontal") != 0)
        {
            targetRotation = Quaternion.AngleAxis(rotationVelocity * Input.GetAxis("Horizontal") * Time.deltaTime, Vector3.up);
        }

        transform.rotation *= targetRotation;

        if (Input.GetAxis("Vertical") != 0)
        {
            rb.velocity += transform.forward * Input.GetAxis("Vertical") * speed;
        }

        //if (Input.GetAxis("Vertical") != 0)
        //{
        //    //rb.AddForce(transform.forward * speed);
        //}
    }
}
