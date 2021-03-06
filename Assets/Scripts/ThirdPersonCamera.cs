﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public float mouseSensitivity;
    public float degreeLimit;
    public GameObject target;

    private Vector3 focus;
    private bool IsRotating;

    private const float MIN_DIST = 20.0f;
    private const float MAX_DIST = 42.0f;

    public float distance = 30.0f;
    public float scrollspeed = 0.2f;

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        this.focus = target.transform.position;
    }

    void Update()
    {
        distance += Input.GetAxis("Mouse ScrollWheel") * scrollspeed;
        distance = Mathf.Clamp(distance, MIN_DIST, MAX_DIST);
        Camera.main.fieldOfView = distance;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Moved)
        {
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");

            // If the right click mouse button is down, rotate
            if (Input.GetMouseButton(0))
            {
                IsRotating = true;
            }
            else
            {
                IsRotating = false;
            }

            if (IsRotating)
            {
                // Rotate the camera around the object based on the X and Y offset in mouse input
                var offset = this.transform.position - focus;

                // Rotate in the X axis
                transform.RotateAround(focus, target.transform.up, mouseSensitivity * mouseX/3);

                if (Mathf.Abs(offset.x) < degreeLimit && Mathf.Abs(offset.z) < degreeLimit && offset.y > 0 && mouseY < 0)
                {
                    return;
                }
                if (Mathf.Abs(offset.x) < degreeLimit && Mathf.Abs(offset.z) < degreeLimit && offset.y < 0 && mouseY > 0)
                {
                    return;
                }
                // Rotate in Y axis
                transform.RotateAround(focus, Vector3.Cross(offset, target.transform.up), -mouseSensitivity * mouseY);

                // Realign the camera so it's not tilted
                transform.Rotate(new Vector3(0, 0, -transform.eulerAngles.z));


            }

    }

}



}