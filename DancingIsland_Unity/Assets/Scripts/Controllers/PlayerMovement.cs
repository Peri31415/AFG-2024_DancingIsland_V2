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

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xMovement + transform.forward * zMovement;

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
        AudioManager.instance.playerSteps.MaterialChecking();

        //Update if charachter is running
        if (Input.GetKey(KeyCode.LeftShift))
            AudioManager.instance.playerSteps.setRunningTrue();
        else
            AudioManager.instance.playerSteps.setRunningFalse();

        //Steps
        if (zMovement != 0 && isGrounded)
            AudioManager.instance.playerSteps.playSteps();
        else
            AudioManager.instance.playerSteps.stopSteps();

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
           AudioManager.instance.playerSteps.jump();
        }
    }
}
