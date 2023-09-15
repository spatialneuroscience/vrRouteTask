using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endmssge : MonoBehaviour
{
    public GameObject Endpoint;
    // Start is called before the first frame update
    void Start()
    {
        Endpoint.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            Endpoint.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        Endpoint.SetActive(false);


    }

}