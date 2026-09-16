using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    AudioSource audioSource;
    [SerializeField] AudioClip mainEngine;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        ProcessThrust();
        ProcessRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                //audioSource.Play();
                audioSource.PlayOneShot(mainEngine);
            }
           
            
            //rb.AddRelativeForce(vector3.up);

            Debug.Log("Thrusting");
        }
        //else
        //{
        //   audioSource.Stop();
        //}
        
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //rb.freezeRotation = true;
            transform.Rotate(Vector3.forward *mainThrust*Time.deltaTime);
            //rb.freezeRotation = false;
            Debug.Log("pressed right");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward *mainThrust*Time.deltaTime);
            Debug.Log("pressed left");
        }
    }
}
