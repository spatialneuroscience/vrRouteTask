using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warning : MonoBehaviour
{
    private string warn;
    GameObject inputField;
    GameObject textDisplay;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="MainCamera")
        {
            warn= inputField.GetComponent<Text>().text;
            textDisplay.GetComponent<Text>().text = "Wrong! We will put you back to your previous location!";

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
