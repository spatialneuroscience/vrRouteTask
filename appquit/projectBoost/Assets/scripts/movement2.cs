using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement2 : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 1000f;
    AudioSource audiousource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiousource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        processthrust();
        processrotation();
    }
    void processthrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up*speed*Time.deltaTime);
            if (!audiousource.isPlaying)
            {
                audiousource.Play();
            }
            Debug.Log("Thrust");

        }
        else
        {
            audiousource.Stop();
        }
        
        }
    void processrotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward*speed*Time.deltaTime);
            rb.freezeRotation = true;
            Debug.Log("move to left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.freezeRotation = true;
            transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
            rb.freezeRotation = false;
            Debug.Log("move to the right");
        }
    }
    }

