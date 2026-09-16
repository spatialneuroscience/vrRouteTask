using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loops : MonoBehaviour
{
    GameObject player;
    float distance;
    public GameObject destroy1, destroy2, destroy3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= distance)
        {
            Destroy(destroy1, 0f);
            Destroy(destroy2, 0f);
            Destroy(destroy3, 0f);
        }
    }
}
