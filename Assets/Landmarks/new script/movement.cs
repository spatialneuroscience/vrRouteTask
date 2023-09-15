using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float mive = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(new Vector3(move, 0, 0));
        transform.Translate(new Vector3(0, 0, mive));



    }
}
