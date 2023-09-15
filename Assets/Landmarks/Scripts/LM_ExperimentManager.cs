﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LM_ExperimentManager : MonoBehaviour
{
    
    public Toggle practice;
    public TMP_Dropdown ui;
    public TMP_InputField subID;
    public TMP_Dropdown condition;
    public TMP_Dropdown environment;
    public Button start;

    private string expID;
    private string appDir = "";
    private bool expDirCreated = false;
    private bool subDirCreated = false;
    private Config config;

    private bool expidError = true;
    private bool subidError = true;
    private bool uiError = true;

    void Start()
    {

        config = Config.instance;
        Debug.Log(config.name);

        //appDir = Directory.GetCurrentDirectory();

        //// check if there is a /data/ folder in our project; create if necessary
        //if (!Directory.Exists(appDir + "/data/"))
        //{
        //    Directory.CreateDirectory(appDir + "/data/");
        //}
        appDir = Application.persistentDataPath;

        expID = config.experiment;

        // FIXME populate the dropdowns for level and condition

        // FIXME

        start.onClick.AddListener(LoadExperiment);

    }

    private void Update()
    {
        practice.onValueChanged.AddListener(delegate { TogglePracticeState(practice); });

    }


    //--------------------------------------------------------------------------
    // Validate Subject ID
    //--------------------------------------------------------------------------

    public void ValidateSubjectID()
    {
        TextMeshProUGUI _errorMessage = subID.transform.Find("Error").GetComponent<TextMeshProUGUI>();

        // check if a subID was even provided
        if (subID.text != "")
        {
            // if so, make sure it's an int
            if (int.TryParse(subID.text, out int _subID))
            {

                // If this id has already been used to save data, flag an error
                if (Directory.Exists(appDir + "/" + expID + "/" + subID.text) && !practice.isOn)
                {
                    subidError = true;
                    _errorMessage.text = "That SubjectID is already in use.";
                    _errorMessage.gameObject.SetActive(true);
                }
                else
                {
                    subidError = false;
                    _errorMessage.gameObject.SetActive(false); // then and only then, will we release the flag
                }
            }
            // if the subID is not an int, throw the message to fix
            else
            {
                subidError = true;
                _errorMessage.text = "Subject ID must be an integer.";
                _errorMessage.gameObject.SetActive(true);
            }
        }
        else
        {
            subidError = true;
            _errorMessage.text = "You must provide a Subject ID.";
            _errorMessage.gameObject.SetActive(true);
        }
    }


    //--------------------------------------------------------------------------
    // Validate UI selected (make sure they selected one)
    //--------------------------------------------------------------------------

    public void ValidateUI()
    {
        //if (FindObjectOfType<)
        //{

        //}
        TextMeshProUGUI _errorMessage = ui.transform.Find("Error").GetComponent<TextMeshProUGUI>();

        if (ui.value != 0)
        {
            uiError = false;
            _errorMessage.gameObject.SetActive(false);
        }
        else
        {
            uiError = true;
            _errorMessage.text = "Please select a UI from the dropdown.";
            _errorMessage.gameObject.SetActive(true);
        }
    }

    public void LoadExperiment()
    {
        TextMeshProUGUI startErrorMessage = start.transform.Find("Error").GetComponent<TextMeshProUGUI>();

        Debug.Log("trying to load experiment");

        ValidateSubjectID();
        ValidateUI();

        if (!expidError && !subidError && !uiError)
        {
            startErrorMessage.gameObject.SetActive(false);

            // Create the directories if they don't exist
            if (!Directory.Exists(appDir + "/" + expID))
            {
                Directory.CreateDirectory(appDir + "/" + expID);
                expDirCreated = true;
            }

            if (practice.isOn)
            {
                if (!Directory.Exists(appDir + "/" + expID + "/practice"))
                {
                    Directory.CreateDirectory(appDir + "/" + expID + "/practice");
                }
            }
            else
            {
                Directory.CreateDirectory(appDir + "/" + expID + "/" + subID.text);
                subDirCreated = true;
            }

            readyConfig();
            ReadyExp();
            SceneManager.LoadScene(config.level);
        }
        else
        {
            startErrorMessage.gameObject.SetActive(true);
        }
    }

    // Feed parameters to Config for loading experiments

    void ReadyExp()
    {
        config.levelNames = new List<string>();
        config.levelNames.Add(environment.options[environment.value].text);
        config.level = environment.options[environment.value].text;

        config.conditions = new List<string>();
        config.conditions.Add(condition.options[condition.value].text);
    }



    //--------------------------------------------------------------------------
    // set up our config for the LM experiment
    //--------------------------------------------------------------------------
    void readyConfig()
    {
        config.runMode = ConfigRunMode.NEW;
        config.bootstrapped = true;

        config.appPath = appDir;
        config.subject = subID.text;
        config.ui = ui.options[ui.value].text;

        DontDestroyOnLoad(config);

    }

    void TogglePracticeState(Toggle toggle)
    {
        if (toggle.isOn)
        {
            // ValidateSubjectID();
        }

    }
}
