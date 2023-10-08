using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    [SerializeField] private float speed;
    [SerializeField] private float rpm;
    [SerializeField] private float horsePower;
    private const float turnSpeed = 35.0f;

    private float horizontalInput;
    private float forwardInput;

    public Camera mainCamera;
    public Camera hoodCamera;

    public KeyCode switchKey;
    public string inputID;

    private Rigidbody playerRB;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.centerOfMass = centerOfMass.transform.localPosition;
    }
    void Awake()
    {
        //must give each wheel a little torque or the wheel colliders will be stuck/not work properly
        foreach (WheelCollider wheel in allWheels)
        {
            wheel.motorTorque = 0.000001f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Player Input
        horizontalInput = Input.GetAxis("Horizontal" + inputID);
        forwardInput = Input.GetAxis("Vertical" + inputID);

        if (IsOnGround())
        {
            // Move the vehicle forward
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            playerRB.AddRelativeForce(Vector3.forward * horsePower * forwardInput);

            //Turn the vehicle
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

            //print speed
            speed = Mathf.RoundToInt(playerRB.velocity.magnitude * 3.6f); // For mph multiply by 2.237f
            speedometerText.SetText("Speed: " + speed + " kph");

            //print RPM
            rpm = (speed % 30) * 40;
            rpmText.SetText("RPM: " + rpm);
        }
        //Input for camera switch
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
    bool IsOnGround()
    {
        wheelsOnGround = 0;

        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround > 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}