using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool lerpCrouch;
    private bool grounded;
    private float crouchTimer;
    private bool crouching = false;
    private bool sprinting = false;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    private float movementSpeed;
    public float baseMovementSpeed = 5f;
    public float sprintMovementSpeed = 8f;
    public float crouchMovementSpeed = 3f;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        movementSpeed = baseMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded;
        
        if (lerpCrouch)
        {
            CrouchLerp();
        }
         
    }

    public void ToggleCrouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;

        if (crouching)
        {
            movementSpeed = crouchMovementSpeed;
        }
        else
        {
            movementSpeed = baseMovementSpeed;
        }
    }

    private void CrouchLerp()
    {
        crouchTimer += Time.deltaTime;
        float p = crouchTimer / 1;
        p *= p;

        if (crouching)
        {
            controller.height = Mathf.Lerp(controller.height, 1, p);
        }
        else
        {
            controller.height = Mathf.Lerp(controller.height, 2, p);
        }

        if (p > 1)
        {
            lerpCrouch = false;
            crouchTimer = 0f;
        }
    }

    public void ToggleSprint()
    {
        crouching = false;
        lerpCrouch = true;
        sprinting = !sprinting;

        if (sprinting)
        {
            movementSpeed = sprintMovementSpeed;
        }
        else
        {
            movementSpeed = baseMovementSpeed;
        }
    }

    // Recieve the inputs for our InputManager.cs and apply them to our character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection * movementSpeed * Time.deltaTime));
        playerVelocity.y += gravity * Time.deltaTime;
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // Handle jumping
    public void Jump()
    {
        if (grounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }
}
