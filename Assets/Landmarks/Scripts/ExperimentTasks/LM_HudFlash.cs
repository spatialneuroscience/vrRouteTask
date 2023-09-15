/*
    LM Dummy
       
    Attached object holds task components that need to be effectively ignored 
    by Tasklist but are required for the script. Thus the object this is 
    attached to can be detected by Tasklist (won't throw error), but does nothing 
    except start and end.   

    Copyright (C) 2019 Michael J. Starrett

    Navigate by StarrLite (Powered by LandMarks)
    Human Spatial Cognition Laboratory
    Department of Psychology - University of Arizona   
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LM_HudFlash : ExperimentTask
{
    [Header("Task-specific Properties")]
    public Color flashColor;
    public float flashDuration;

    private float startTime;
    private Color hudBaseColor;

    public override void startTask()
    {
        TASK_START();

        // LEAVE BLANK
    }


    public override void TASK_START()
    {
        if (!manager) Start();
        base.startTask();

        if (skip)
        {
            log.log("INFO    skip task    " + name, 1);
            return;
        }

        // record the start time
        startTime = Time.time;

        // WRITE TASK STARTUP CODE HERE
        hudBaseColor = hud.hudPanel.GetComponent<Image>().color;
        hud.hudPanel.GetComponent<Image>().color = flashColor;
        hud.showOnlyHUD();
    }


    public override bool updateTask()
    {

        if (Time.time - startTime > flashDuration)
        {
            //endTask();
            return true;
        }
        else return false;

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
        // clean up
        hud.hudPanel.GetComponent<Image>().color = hudBaseColor;
        
    }

}