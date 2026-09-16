#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DebugButton : EditorWindow
{
    [MenuItem("Tools/Debug")]
    public static void QuickMove()
    {
        DebugButton wnd = GetWindow<DebugButton>();
    }

    public void CreateGUI()
    {
        var root = rootVisualElement;
        var loadButton = new Button(Load)
        {
            text = "Load"
        };
        var skipButton = new Button(Skip)
        {
            text = "Skip"
        };
        root.Add(skipButton);
    }

    private List<GameObject> _mover = new List<GameObject>();
    private void Load()
    {
        _mover.Clear();
        _mover = GameObject.FindGameObjectsWithTag("mover").ToList();
    }

    private void Skip()
    {
    }
}
#endif