using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Blockwall")
        {
            Debug.Log("tRIGGERING THE SWITCH!");
            transform.position = Vector3.MoveTowards(transform.position, checkpoints.transform.position, speed * Time.deltaTime);
        }
    }
    public static Vector3 LastCheckPointPos;
    public GameObject MainCamera;
    private GameObject checkpoints;
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
