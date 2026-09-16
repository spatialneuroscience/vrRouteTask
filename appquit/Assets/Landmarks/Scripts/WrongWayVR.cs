using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WrongWayVR : MonoBehaviour
{
    public GameObject UIObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if (other == GameObject.FindGameObjectWithTag("Player").GetComponent<LM_PlayerController>().collisionObject)
        {
            if (UIObject.activeSelf == true)
            {
                UIObject.SetActive(false);

            }
            else
            {
                UIObject.SetActive(true);

            }
        }
        if (other.tag == "Player")
        {
            if (UIObject.activeSelf == true)
            {
                UIObject.SetActive(false);

            }
            else
            {
                UIObject.SetActive(true);

            }


        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
