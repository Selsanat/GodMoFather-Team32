using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static Unity.Collections.Unicode;

public class CarMovement : MonoBehaviour
{
    public InputAction carControls;
    private Rigidbody2D rb;
    bool canTurn = false;
    float newrotationZ;
    float direction;
    Vector2 input;

     [Header("Movement Parameters")]
    [SerializeField, Min(0f)] float maxSpeed = 5f;
    [SerializeField , Range(0,1)] float turnTime = 1f;
    [SerializeField, Range(0, 1)] float acceleration=3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    //private void OnEnable()
    //{
    //    Debug.Log("Enabled");
    //    carControls.Enable();
    //    carControls.canceled += ctx => direction = 0;
    //    carControls.performed += ctx =>  ChangeRotation(ctx.ReadValue<float>());
    //}
    //private void OnDisable()
    //{
    //    carControls.Disable();
    //    carControls.canceled -= ctx => direction = 0;
    //    carControls.performed -= ctx => ChangeRotation(ctx.ReadValue<float>());
    //}
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        float newRotation = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg - 90f;
        
        float time =+ Time.deltaTime;

        if(time< turnTime)
        {
            float tAcceleration = Mathf.Clamp01(time / acceleration);
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, input.normalized * maxSpeed, tAcceleration);
            
        }
        if (input.magnitude > 0)
        {
            float tRotation = Mathf.Clamp01(time/turnTime);
            rb.rotation = Mathf.LerpAngle(rb.rotation, newRotation, tRotation);
        }


        //rb.linearVelocity = input.normalized*maxSpeed;




    }

    void FixedUpdate()
    {

    }
    private void ChangeRotation(float coeff)
    {
        // newrotationZ = rb.rotation + (coeff * 90);
        
        //transform.rotation = Quaternion.Euler(0, 0, newrotationZ%360);
        //canTurn = true;


        //Vector2 dir = Vector3.zero;
        //Debug.Log(transform.rotation.eulerAngles.z);
        //switch (transform.rotation.eulerAngles.z)
        //{
        //    case 0:
        //        dir = new Vector2(0, Time.fixedDeltaTime * speed);
        //        break;
        //    case 90:
        //        dir = new Vector2(Time.fixedDeltaTime * -speed, 0);
        //        break;
        //    case 180:
        //        dir = new Vector2(0, Time.fixedDeltaTime * -speed);
        //        break;
        //    case 270:
        //        dir = new Vector2(Time.fixedDeltaTime * speed, 0);
        //        break;
        //}

        //rb.linearVelocity = dir;
        //canTurn = false;
    }



}
