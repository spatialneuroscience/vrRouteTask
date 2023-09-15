using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class newserializer : MonoBehaviour
{
    // Start is called before the first frame update
    private string subjectID;
    
    int interval = 10;

    private string _filename;
    // static readonly string SAVE_FILE = "player.txt";

    private int _time = 0;

    private float startTime;
    private float currentTime;


    void Start()
    {

        startTime = Time.time;
      //  subjectID = InputFieldChat.text;
        _filename = @"C:\Users\Marjan\Documents\unity\Landmarks\test2" + subjectID + ".txt";
        Debug.Log(_filename);
        using (StreamWriter sw = File.CreateText(_filename))
        {

            sw.WriteLine("Subject ID: " + subjectID);
        }
    }

    void Update()
    {
        currentTime = Time.time - startTime;
        //savedata data = new savedata();


        if (_time > interval)
        {
            float x = transform.position.x;
            float z = transform.position.z;
            StreamWriter sw = File.AppendText(_filename);
            sw.WriteLine("X: " + x + ", " + "Z: " + z + "," + "Time:" + currentTime + " ");
            sw.Close();
            _time = 0;
        }
        else

            _time++;

        {
        }
    }
}
