using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WrongWay : MonoBehaviour
{
    public GameObject UIObject;
    public dbLog log;

    // Start is called before the first frame update
    void Start()
    {
        if (!UIObject)
        {
            Debug.LogWarning($"UIObject not set in {gameObject.name}");
        }
    }
    void OnTriggerEnter(Collider other){
        if (UIObject == null)
        {
            return;
        }

        if(other.tag == "Player"){
            if (UIObject.activeSelf == true)
            {
                UIObject.SetActive(false);

            }
            else
            {
                UIObject.SetActive(true);
            }
            


        }
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

    }

    // Update is called once per frame
    void Update()
    {

    }

}
