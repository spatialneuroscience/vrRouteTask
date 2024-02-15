using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordMazeSection : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider targetCollider;
    private LM_PlayerController playerController;
    private dbLog log;
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<LM_PlayerController>();
        targetCollider = playerController.collisionObject;
        log = GameObject.FindWithTag("Experiment").GetComponent<Experiment>().dblog;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var nameSplit = name.Split('-');
        var from = nameSplit[0];
        var to = nameSplit[1];

        log.log("CHANGE SECTION\t" + from + "\t" + to, 1);
        Debug.Log("CHANGE SECTION\t" + from + "\t" + to);

    }
}
