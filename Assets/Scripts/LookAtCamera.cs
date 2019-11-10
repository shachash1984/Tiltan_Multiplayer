﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    private Transform mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (!mainCamera)
            return;

        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.position);
    }
}
