using System.Collections;
using System.Collections.Generic;
using FMODUnityResonance;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float sprintVelocity = 1.15f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    FMOD.Studio.EventInstance jumpSound; //Jump event code declaration for later definition

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * speed * Time.deltaTime * sprintVelocity);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);            
        }
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        //AUDIO
        //Update walking material surface param
        GetComponent<PlayerSteps>().MaterialChecking();

        //Update if charachter is running
        if (Input.GetKey(KeyCode.LeftShift))
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IsRunning", 1);
        else
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IsRunning", 0);

        //Steps
        if (z != 0 && isGrounded)
            GetComponent<PlayerSteps>().StartEvent();
        else
            GetComponent<PlayerSteps>().StopEvent();

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //create and trigger fpp jump FMOD event
            jumpSound = FMODUnity.RuntimeManager.CreateInstance("event:/Foley/FPP/Jump");
            jumpSound.start();
            jumpSound.release();
        }
    }
}
