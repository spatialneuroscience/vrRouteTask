using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;


public class serializer : MonoBehaviour
{
    //static readonly string SAVE_FILE = "player.txt";
    public string subjectID;
    private string _filename;



    public GameObject player;

    int time = 0;
    int interval = 4;
    private float startTime;
    private float currentTime;
    public InputField inputfieldchat;
    




    void Start()
    {

        float startTime = Time.time;
        subjectID = inputfieldchat.text;
        //_filename = @"C:\Users\Marjan\Documents\unity\Landmarks" + subjectID + ".txt"
       _filename =subjectID + ".txt";
       // Debug.Log(_filename);
       using (StreamWriter sw = File.CreateText(_filename))
        {
           sw.WriteLine("Subject ID: " + subjectID);
       }



    }
   
   
    public void readinputfield(string subjectID)
    {
        subjectID = inputfieldchat.text;
        //_filename = @"C:\Users\Marjan\Documents\unity\Landmarks" + subjectID + ".txt"
        _filename = subjectID + ".position.txt";
        // Debug.Log(_filename);
        using (StreamWriter sw = File.CreateText(_filename))
        {
            sw.WriteLine("Subject ID: " + subjectID);
        }
    }

    void Update()
    {
       
        float currentTime = Time.time - startTime;
       // savedata data = new savedata();
        {

           // name = "participants";
            //Vector3 position = player.transform.position;
        }


        if (time > interval)
        {


            float x = transform.position.x;
            float z = transform.position.z;
            StreamWriter sw = File.AppendText(_filename);
            //StreamWriter sw = File.AppendText(SAVE_FILE);
            sw.WriteLine("X:" + x + ", " + "Z:" + z + "," + "Time:" + currentTime);
            sw.Close();
            Debug.Log("write to file");
            time = 0;
        }
        else

            time++;
        {

        }

    }
    
  

    }



       



