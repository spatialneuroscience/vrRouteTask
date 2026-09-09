using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Landmarks.Scripts.Progress;

public class WrongWay : MonoBehaviour
{
    public GameObject UIObject;
    public dbLog log;
    public GameObject guardObject;

    private static bool _armed = false;

    public static void SetArmed(bool armed) { _armed = armed; }

    private string GetUIObjectPath()
    {
        var path = UIObject.name;
        var parent = UIObject.transform.parent;
        while (parent != null) { path = parent.name + "/" + path; parent = parent.parent; }
        return path;
    }

    void Start()
    {
        if (!UIObject) { Debug.LogWarning($"UIObject not set in {gameObject.name}"); return; }
        if (guardObject == null)
        {
            guardObject = GameObject.Find("filler_props");
            if (guardObject == null) Debug.LogWarning($"Could not find filler_props for {gameObject.name}");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!_armed) return;
        if (guardObject != null && !guardObject.activeInHierarchy) return;
        if (UIObject == null) return;

        bool isPlayer = other.tag == "Player" ||
                        other == GameObject.FindGameObjectWithTag("Player")
                            .GetComponent<LM_PlayerController>().collisionObject;

        if (isPlayer)
        {
            bool newState = !UIObject.activeSelf;
            UIObject.SetActive(newState);
            
            LM_Progress.Instance.RecordColliderHit("WW|" + GetUIObjectPath() + "|" + (newState ? "1" : "0") + "|" + DateTime.Now.Ticks);
        }
    }

    void Update() { }
}