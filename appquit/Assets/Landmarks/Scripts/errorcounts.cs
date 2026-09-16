using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class errorcounts : MonoBehaviour
{
    //static readonly string SAVE- FILE = "error.txt";
    private float startTime;
    private float currentTime;
    int time = 0;
    int interval = 4;
    public InputField inputfieldchat;
    public string subjectID;
    private string _file;

    void Start()
    {
        float sartTime = Time.time;
        subjectID = inputfieldchat.text;
        //_filename = @"C:\Users\Marjan\Documents\unity\Landmarks" + subjectID + ".txt"
        _file = subjectID + ".txt";
        // Debug.Log(_filename);
        using (StreamWriter sw = File.CreateText(_file))
        {

            sw.WriteLine("Subject ID: " + subjectID);
        }

    }
    public void readinputfield(string subjectID)
    {
        subjectID = inputfieldchat.text;
        //_filename = @"C:\Users\Marjan\Documents\unity\Landmarks" + subjectID + ".txt"
        _file = subjectID + ".error.txt";
        // Debug.Log(_filename);
        using (StreamWriter sw = File.CreateText(_file))
        {
            sw.WriteLine("Errors: " + subjectID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Blockwall")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(3)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(3):" + "Time:" + currentTime);
            sw.Close();

            Debug.Log("errors detected");
        }
        if (other.gameObject.tag == "wall4")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(4)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(4):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall5")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(5)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(5):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall6")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(6)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(6):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall7")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(7)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(7):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall8")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(8)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(8):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall9")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(9)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(9):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall10")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(10)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(10):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall11")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(11)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(11):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall13")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(13)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(13):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall14")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(14)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(14):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall15")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(15)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(15):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall16")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(16)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(16):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall17")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(17)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(17):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall18")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(18)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(18):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall19")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(19)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(19):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall20")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(20)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(20):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall21")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(21)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(21):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall22")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(22)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(22):" + "Time:" + currentTime);
            sw.Close();
        }

        if (other.gameObject.tag == "cubeb")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cubeb");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cubeb:" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall24")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(24)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(24):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall25")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(25)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(25):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall26")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(26)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(26):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall27")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(27)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(27):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall28")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(28)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(28):" + "Time:" + currentTime);
            sw.Close();
        }
        if (other.gameObject.tag == "wall29")
        {
            currentTime = Time.time - startTime;
            Debug.Log("I bumped into the cube(29)");
            StreamWriter sw = File.AppendText(_file);
            sw.WriteLine("cube(29):" + "Time:" + currentTime);
            sw.Close();
        }

        void Update()
    {
        float currentTime = Time.time - startTime;
        }
        if (time > interval)
        {
           
        }
        else
            time++;
        
        }
      

        

        }

    


