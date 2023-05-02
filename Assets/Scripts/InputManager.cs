using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControlsScript playerControlsScript;

    public Vector2 leftStickInput;
    public Vector2 rightStickInput;

    public float movementInputX;  // horizontalInput
    public float movementInputY;  // verticalInput

    public float cameraInputX;
    public float cameraInputY;

    public bool lockOnInput = false;
    public bool jumpInput = false;
    public bool sprintInput = false;
    public bool selectInput = false;
    public bool attackInput = false;

    private void Update()
    {
        HandleAllInputs();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();
    }

    private void HandleMovementInput()
    {
        movementInputX = leftStickInput.x;
        movementInputY = leftStickInput.y;
    }

    private void HandleCameraInput()
    {
        cameraInputX = rightStickInput.x;
        cameraInputY = rightStickInput.y;
    }

    private void OnEnable()
    {
        if (playerControlsScript == null)
        {
            playerControlsScript = new PlayerControlsScript();

            playerControlsScript.PlayerControls.Movement.performed += ctx => leftStickInput = ctx.ReadValue<Vector2>();
            playerControlsScript.PlayerControls.Camera.performed += ctx => rightStickInput = ctx.ReadValue<Vector2>();

            playerControlsScript.PlayerControls.Jump.performed += ctx => jumpInput = true;
            playerControlsScript.PlayerControls.Jump.canceled += ctx => jumpInput = false;

            playerControlsScript.PlayerControls.Sprint.started += ctx => sprintInput = true;
            playerControlsScript.PlayerControls.Sprint.canceled += ctx => sprintInput = false;

            playerControlsScript.PlayerControls.Select.started += ctx => selectInput = true;
            playerControlsScript.PlayerControls.Select.canceled += ctx => selectInput = false;

            playerControlsScript.PlayerControls.Attack.started += ctx => attackInput = true;
            playerControlsScript.PlayerControls.Attack.canceled += ctx => attackInput = false;

            playerControlsScript.PlayerControls.LockOn.started += ctx => lockOnInput = true;
            playerControlsScript.PlayerControls.LockOn.canceled += ctx => lockOnInput = false;
        }
        
        playerControlsScript.Enable();
    }

    private void OnDisable()
    {
        playerControlsScript.Disable();
    }
}
