using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// using UnityEngine.UIElements;

public class PlayerLook : MonoBehaviour
{
    public Camera playerCamera;
    public Camera deathCamera;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    // Start is called before the first frame update
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Rotate player up and down
        xRotation -= mouseY * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        
        // Rotate player left and right
        transform.Rotate(Vector3.up * (mouseX * xSensitivity));
    }
}
