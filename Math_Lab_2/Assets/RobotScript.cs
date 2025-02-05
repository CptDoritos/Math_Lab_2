using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxSteeringAngle = 30f;
    public float wheelRotationSpeed = 360f;
    public float rotationAngle = 0f;
    public float rotationAmount = 0f;
    public int turning = 2;

    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform[] wheels;

    private float currentSpeed;
    private float currentSteering;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed = moveSpeed;
            rotationAmount = wheelRotationSpeed;
        }else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentSpeed = moveSpeed * -1;
            rotationAmount = wheelRotationSpeed * -1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = 0f;
            rotationAmount = 0f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentSteering = maxSteeringAngle;
            turning = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentSteering = maxSteeringAngle * -1;
            turning = 0;
        }
        else 
        {
            currentSteering = 0f;
            if (turning == 0) 
            {
                turning = 1;
            }
        }

        if (turning == 0)
        {
            frontRightWheel.localRotation = Quaternion.Euler(90, (currentSteering + 90), 0);
            frontLeftWheel.localRotation = Quaternion.Euler(90, (currentSteering + 90), 0);
        }
        else if (turning == 1)
        {
            frontRightWheel.localRotation = Quaternion.Euler(90, 90, 0);
            frontLeftWheel.localRotation = Quaternion.Euler(90, 90, 0);
            turning = 2;
        }
        else
        {

        }

        if (currentSpeed != 0)
        {
            rotationAngle = currentSteering * Time.deltaTime * Mathf.Sign(currentSpeed);
            transform.Rotate(Vector3.up, rotationAngle);
            foreach (Transform wheel in wheels)
            {
                wheel.Rotate(Vector3.up, rotationAmount * Time.deltaTime);
            }
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}
