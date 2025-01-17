using System;
using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
   
    public Transform models;
    public Transform cameraTarget;
    public float speed = 10f;

    public Vector3 direction;

    public float dashSpeed = 20f;
    public float dashTime = 0.5f;

    private bool isDash = true;
    private bool isDashTimer = true;
    private PlayerInputHandler InputHandler;
    private CharacterController controller;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        InputHandler = PlayerInputHandler.Instance;
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        PlayerLook();
    }
    private void Move()
    {
        direction = new Vector3(InputHandler.MoveInput.x, 0, InputHandler.MoveInput.y).normalized;

   
        controller.Move(direction * speed * Time.deltaTime);
       
        if(Input.GetKey(KeyCode.LeftShift) && isDash)
        {
            isDash = false;
            StartCoroutine(DashTimer());
            controller.Move(direction * dashSpeed * Time.deltaTime);
        }
    }
    private IEnumerator DashTimer()
    {
        if(isDashTimer)
        {
            isDashTimer = false;
            yield return new WaitForSeconds(dashTime);
            isDash = true;
            isDashTimer = true;
        }
        
    }

    private void PlayerLook()
    {
        Vector3 lookDirection = cameraTarget.position - models.position;
        lookDirection.y = 0;
        models.rotation = Quaternion.LookRotation(lookDirection);
        
    }

}
