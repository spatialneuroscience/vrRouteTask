#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
public class CSVGameObjectRenamer : EditorWindow
{
    GameObject selectedGameObject;
    string filePath = string.Empty;

    [MenuItem("Tools/CSV GameObject Renamer")]
    public static void ShowWindow()
    {
        GetWindow<CSVGameObjectRenamer>("CSV GameObject Renamer");
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        selectedGameObject = (GameObject)EditorGUILayout.ObjectField("GameObject", selectedGameObject, typeof(GameObject), true);
        filePath = EditorGUILayout.TextField("Path to CSV File", filePath);

        if (GUILayout.Button("Rename GameObjects"))
        {
            ReadCSVAndRename(filePath);
        }

        if (GUILayout.Button("Add Record Wrong way components after renaming"))
        {
            AddComponents(filePath);
        }
    }

    void ReadCSVAndRename(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("File not found: " + path);
            return;
        }

        string[] lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++) // Start at 1 to skip header
        {
            string[] columns = lines[i].Split(',');
            if (columns.Length < 3) continue; // Skip if row does not have enough columns

            string partNumber = columns[0];
            string cubeName = columns[1];
            string finalName = columns[2];

            GameObject partGameObject = GameObject.Find($"part{partNumber}");
            if (partGameObject != null)
            {
                Transform cubeTransform = partGameObject.transform.Find($"Cube ({cubeName})");
                if (cubeTransform != null)
                {
                    cubeTransform.gameObject.name = finalName;
                    EditorUtility.SetDirty(cubeTransform.gameObject);
                }
            }
        }
    }

    void AddComponents(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("File not found: " + path);
            return;
        }

        string[] lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++) // Start at 1 to skip header
        {
            string[] columns = lines[i].Split(',');
            if (columns.Length < 3) continue; // Skip if row does not have enough columns

            string partNumber = columns[0];
            string cubeName = columns[1];
            string finalName = columns[2];

            GameObject partGameObject = GameObject.Find($"part{partNumber}");
            if (partGameObject != null)
            {
                Transform cubeTransform = partGameObject.transform.Find(finalName);
                if (cubeTransform != null)
                {
                    RecordWrongWay component;
                    if (cubeTransform.TryGetComponent<RecordWrongWay>(out var wrongWay))
                    {
                        component = wrongWay;
                    }
                    else
                    {
                        component = cubeTransform.gameObject.AddComponent<RecordWrongWay>();
                    }

                    // if final name ends with C or M
                    if (finalName.EndsWith("C") || finalName.EndsWith("M"))
                    {
                        component.SetWrongDirection(false);
                    }
                    else
                    {
                        component.SetWrongDirection(true);
                    }

                    EditorUtility.SetDirty(cubeTransform.gameObject);
                }
            }
        }
    }
}

#endif
