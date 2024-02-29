using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordWrongWay : MonoBehaviour
{
    // Start is called before the first frame update
    private dbLog log;
    [SerializeField] private bool isWrongDirection;
    private Collider firstCollider;
    void Start()
    {

        log = GameObject.FindWithTag("Experiment").GetComponent<Experiment>().dblog;
    }

    public void SetWrongDirection(bool wrong)
    {
        isWrongDirection = wrong;
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (firstCollider == null)
        {
            firstCollider = other;
        }
        else if (firstCollider != other)
        {
            return;
        }

        if (isWrongDirection)
        {
            log.log("WRONG WAY\t"+name, 1);
            Debug.Log("WRONG WAY\t"+name);
        }
        else
        {
            log.log("CORRECT WAY\t"+name, 1);
            Debug.Log("CORRECT WAY\t"+name);
        }
    }
}
