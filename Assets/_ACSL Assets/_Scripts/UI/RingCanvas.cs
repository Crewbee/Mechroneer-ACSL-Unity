﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class RingCanvas : MonoBehaviour
{
    public bool alwaysFaceCamera;

    private Canvas _canvas;
    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        FindUIInfoInChildren(transform);
        _canvas.worldCamera = Camera.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (alwaysFaceCamera)
        {
            Vector3 cameraEuler = Camera.main.transform.rotation.eulerAngles;

            cameraEuler = cameraEuler - gameObject.transform.rotation.eulerAngles;

            cameraEuler.x = 0f;
            cameraEuler.z = 0f;

            gameObject.transform.Rotate(cameraEuler, Space.Self);
        }
    }

    private void FindUIInfoInChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            FindUIInfoInChildren(child);
            //UIPanelOld info = child.GetComponent<UIPanelOld>();
            //info.PanelInit();
            //if (info)
            //{
            //    info.overlay = true;
            //}
        }
    }
}