using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class newwarn : MonoBehaviour
{
    [SerializeField] public Text uiObject;

    private float _time;

    private void Start()
    {
        uiObject.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            StartCoroutine(ShowMessage());
            uiObject.enabled = true;
        }
    }

    private IEnumerator ShowMessage()
    {
      
        yield return new WaitForSeconds(5);

        uiObject.enabled = false;
    }
}