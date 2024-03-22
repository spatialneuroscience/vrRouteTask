using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _cam;
    private Canvas _canvas;
    [SerializeField] private float distance = 3f;
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var camTransform = _cam.transform;
        var pos = camTransform.position;
        var forward = camTransform.forward;
        var up = camTransform.up;

        transform.position = pos + forward * distance;
        transform.rotation = Quaternion.LookRotation(forward, up);
    }

    private void OnEnable()
    {
    }
}
