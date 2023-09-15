using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class SUBINFO : ExperimentTask
{
    public TMP_InputField subjectnumber_input, learningphase_input;
    private int subjectnumber;
    private int learningphase;
    public void subjectinfo()
    {
        subjectnumber = Int32.Parse(subjectnumber_input.text);
        learningphase= Int32.Parse(learningphase_input.text);
    }
    public override void startTask()
    {
        TASK_START();

        // LEAVE BLANK
    }


    public override void TASK_START()
    {
        if (!manager) Start();
        base.startTask();

        // WRITE TASK STARTUP CODE HERE
    }


    public override bool updateTask()
    {
        return true;

        // WRITE TASK UPDATE CODE HERE
    }


    public override void endTask()
    {
        TASK_END();

        // LEAVE BLANK
    }


    public override void TASK_END()
    {
        base.endTask();

        // WRITE TASK EXIT CODE HERE
    }

} 
    

