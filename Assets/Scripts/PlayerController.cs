using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    ButtonLogic buttonLogic;

    public GameObject CutsceneCam;

    public Vector3 moveDirection;
    public Transform cameraTransform;
    public Rigidbody playerRB;

    public float runSpeed = 5;
    public float sprintSpeed = 10;
    public float rotationSpeed = 10.0f;

    public float jump = 10f;
    public bool onGround = true;

    public void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();

        buttonLogic = FindObjectOfType<ButtonLogic>();
        CutsceneCam = GameObject.Find("CutsceneCam");
        CutsceneCam.SetActive(false);
    }

    void FixedUpdate()
    {
        HandleAllPlayerMovement();
    }
   
    void HandleAllPlayerMovement()
    {
        HandlePlayerRotation();
        HandlePlayerMovement();
        HandleJump();
        HandleSelect();
        HandleAttack();
    }

    void HandlePlayerRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraTransform.forward * inputManager.movementInputY;
        targetDirection = targetDirection + cameraTransform.right * inputManager.movementInputX;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    void HandlePlayerMovement()
    {
        Vector3 forward = transform.InverseTransformVector(cameraTransform.forward);
        Vector3 right = transform.InverseTransformVector(cameraTransform.right);

        forward.y = 0;
        right.y = 0;

        forward = forward.normalized;
        right = right.normalized;

        Vector3 FRVI = inputManager.movementInputY * forward;
        Vector3 RRVI = inputManager.movementInputX * right;

        Vector3 CRM = FRVI + RRVI;

        transform.Translate(CRM * runSpeed * Time.fixedDeltaTime);

    }

    /*        moveDirection = cameraTransform.forward * inputManager.movementInputY;
            moveDirection = moveDirection + cameraTransform.right * inputManager.movementInputX;
            moveDirection.Normalize();
            moveDirection.y = 0f;

            if (inputManager.sprintInput)
            {
                moveDirection = moveDirection * sprintSpeed;
            }
            else
            {
                moveDirection = moveDirection * runSpeed;
            }

            Vector3 movementVelocity = moveDirection;
            playerRB.MovePosition(transform.position + new Vector3(moveDirection.x, moveDirection.y, moveDirection.z) * Time.deltaTime);
        }*/

    //transform.Translate(moveDirection + new Vector3(moveDirection.x, moveDirection.y, moveDirection.z).normalized * runSpeed * Time.deltaTime, Space.World);

    //Kinda Works - is bad
    //playerRB.AddForce(movementVelocity + new Vector3(moveDirection.x, moveDirection.y, moveDirection.z));

    private void HandleSelect()
    {
        if (inputManager.selectInput && buttonLogic.btnPressable)
        {
            CutsceneCam.SetActive(true);
            buttonLogic.DoorOpen();
            inputManager.selectInput = false;
        }

        else if (!buttonLogic.btnPressable) 
        {
            CutsceneCam.SetActive(false);        
        }
    }

    private void HandleAttack()
    {
        if (inputManager.attackInput)
        {
            //animator.Play("Base Layer.MMA Kick", 0, 0.25f);
        }

        else
        {
            //animator.SetBool("IsFighting", false);
        }
    }



    private void HandleJump()
    {
        if (inputManager.jumpInput && onGround)
        {
            playerRB.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
            onGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
}
