using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled= false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
