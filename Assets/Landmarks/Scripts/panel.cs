using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Button;
    int counter;

    public void OpenPanel()
    {
        counter++;
        if (counter % 2 == 1)
        {
            Panel.gameObject.SetActive(false);
        }
        else
        {
            Panel.gameObject.SetActive(true);
            
        }
        {
           // bool isActive = Panel.activeSelf;
            //Panel.SetActive(!isActive);
            //Panel.SetActive(true);




        }
    }
    public void DestroyButton()
    {
        Destroy(Button);
    }
}
