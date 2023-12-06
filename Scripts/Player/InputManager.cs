using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    public PlayerInput.PausedActions paused;
    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerHealth health;
    private PlayerUI ui;
   
    
    // Awake is called before everything else
    void Awake()
    {
        // Set up input
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        paused = playerInput.Paused;

        // Get other scripts
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        health = GetComponent<PlayerHealth>();
        ui = GetComponent<PlayerUI>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Pass movement and look vectors to motor and look
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        
        // OnFoot bindings
        if (onFoot.Jump.WasPressedThisFrame())
        {
            motor.Jump();
        }

        if (onFoot.Crouch.WasPressedThisFrame())
        {
            motor.ToggleCrouch();
        }

        if (onFoot.Sprint.WasPressedThisFrame())
        {
            motor.ToggleSprint();
        }

        if (onFoot.Damage.WasPressedThisFrame())
        {
            health.Damage(30);
        }

        if (onFoot.Heal.WasPressedThisFrame())
        {
            health.Heal(30);
        }

        if (onFoot.Pause.WasPressedThisFrame())
        {
            ui.PauseGame();
            onFoot.Disable();
            paused.Enable();
        }

        // OnPause bindings
        if (paused.Continue.WasPressedThisFrame())
        {
            ui.ContinueGame();
            onFoot.Enable();
            paused.Disable();
        }

    }

    // OnEnable is called just after object is enabled
    private void OnEnable()
    {
        onFoot.Enable();
        paused.Disable();
    }
    
    // OnDisable is called just before object is disabled
    private void OnDisable()
    {
        onFoot.Disable();
        paused.Disable();
    }
}