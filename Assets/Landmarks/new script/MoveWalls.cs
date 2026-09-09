using System.Collections;
using System;
using UnityEngine;
using Landmarks.Scripts.Progress;

public class MoveWalls : MonoBehaviour
{
    public GameObject localWalls;
    public GameObject replacementWalls;
    public Vector3 localTransform;
    public Vector3 replacementTransform;
    public float waitTime;
    public float scalar = 7.4f / 7f;

    private Quaternion _quaternion;

    public string StableId => $"MoveWalls_{gameObject.name}_" +
                               $"{(localWalls ? localWalls.name : "null")}_" +
                               $"{(replacementWalls ? replacementWalls.name : "null")}";

    void Start()
    {
        _quaternion = GameObject.FindGameObjectWithTag("maze").transform.rotation;
    }

    public void ApplyWallSwapImmediate()
    {
        if (localWalls)
            localWalls.transform.position += _quaternion * localTransform * scalar;
        if (replacementWalls)
            replacementWalls.transform.position += _quaternion * replacementTransform * scalar;
    }

    private string GetTriggerPath()
    {
        var path = gameObject.name;
        var parent = transform.parent;
        while (parent != null) { path = parent.name + "/" + path; parent = parent.parent; }
        return path;
    }

    private string GetEventId()
    {
        return "MW|" + GetTriggerPath() +
            "|" + (localWalls ? localWalls.name : "null") +
            "|" + (replacementWalls ? replacementWalls.name : "null") +
            "|" + DateTime.Now.Ticks;
    }

    void OnTriggerEnter(Collider other)
    {
        if (LM_Progress.IsRestoring) return;
        // everything else unchanged

        var exp = GameObject.FindObjectOfType<Experiment>();
        if (exp != null && !exp.goToEnded) return;

        bool isPlayer = other.tag == "Player" ||
                        other == GameObject.FindGameObjectWithTag("Player")
                            .GetComponent<LM_PlayerController>().collisionObject;

        if (isPlayer)
        {
            Debug.Log("TRIGGERING THE SWITCH!");
            StartCoroutine(moveWalls());
            LM_Progress.Instance.RecordColliderHit(GetEventId());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LM_Progress.IsRestoring) return;

        var exp = GameObject.FindObjectOfType<Experiment>();
        if (exp != null && !exp.goToEnded) return;

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("TRIGGERING THE SWITCH!");
            StartCoroutine(moveWalls());
            LM_Progress.Instance.RecordColliderHit(GetEventId());
        }
    }

    IEnumerator moveWalls()
    {
        if (localWalls)
            localWalls.transform.position += _quaternion * localTransform * scalar;
        yield return new WaitForSeconds(waitTime);
        if (replacementWalls)
            replacementWalls.transform.position += _quaternion * replacementTransform * scalar;
    }

    void Update() { }
}