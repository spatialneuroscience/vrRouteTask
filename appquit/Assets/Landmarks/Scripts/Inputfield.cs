using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Inputfield : MonoBehaviour
{
    [HideInInspector] public InputField InputFieldChat;
    static readonly string SAVE_FILE = "player.txt";
    public GameObject player;
    int time = 0;
    int interval = 10;
    private float startTime;
    private float currentTime;
    [HideInInspector] public bool idSubmitted;

    private void Start()
    {
       
        
        Directory.CreateDirectory(Application.streamingAssetsPath + "/Chat_logs/");
        float startTime = Time.time;

    }
   
    void Update()
    {
        float currentTime = Time.time - startTime;
        savedata data = new savedata();
        {

            name = "participants";
            Vector3 position = player.transform.position;
        }
        if (time > interval)
        {

           // string textDocumentName = Application.streamingAssetsPath + "/Chat_logs/" + "subjectnumber" + "positions" + ".txt";
            float x = transform.position.x;
            float z = transform.position.z;
            //StreamWriter sw = File.AppendText(textDocumentName);
            //sw.WriteLine("X:" + x + ", " + "Z:" + z + "," + "Time:" + currentTime);
            //File.AppendAllText(textDocumentName, "x" + transform.position.x + "," + "z" +transform.position.z + "," + "Time" + currentTime);
        }

        
    }
    public void createtextfile()
    {
        if (InputFieldChat.text == "")
        {
           return;
       }
       if (!Directory.Exists(InputFieldChat.text))
      {
            
            Directory.CreateDirectory(InputFieldChat.text);
            }
            string textDocumentName = InputFieldChat.text+ ".txt";
          
        File.WriteAllText(textDocumentName, "Subject ID: ");

        ////string player = InputFieldChat.text + ".txt";

        //////    // string textDocumentName = Application.streamingAssetsPath + "/Chat_logs/" + "subjectnumber" + "positions" + ".txt";
        if (!File.Exists(textDocumentName))
         //// if (!File.Exists(player))
       {
         File.WriteAllText(textDocumentName, "Subject Number:\n\n");
        // File.WriteAllText(player, "Subject Number:\n\n");
        }
        File.AppendAllText(textDocumentName, InputFieldChat.text + "\n");
        //File.AppendAllText(player, InputFieldChat.text + "\n");


        // Debug.LogWarning("HOW IS THIS CODE RUNNING IF THE METHOD IS NEVER CALLED!!!!!!!!!!!!!");
        }


   }

