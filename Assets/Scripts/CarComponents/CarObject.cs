using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObject : MonoBehaviour
{
    public CarCollisionComponent carCollisionComponent;

    //10 > seems to be enough to get over 45 degree angles
    //15 does ramp off
    //20 Can do larger loop-de-loop
    public float forwardSpeed = 1;
    public float InAirfloorAlignSpeed = 1;
    bool isGrounded = false;

    private void Start()
    {
        carCollisionComponent.InitCarCollision(this);
    }

    void FixedUpdate()
    {
        HandleCarMovement();
        OrientToFloor();
    }

    void HandleCarMovement()
    {
        if (isGrounded)
        {
            carCollisionComponent.AddMovementForce(transform.forward * forwardSpeed);
        }        
    }

    public void SetCarForwardRotation(Vector3 _rot)
    {
        //Only set rotation if is a wall
        if(Vector3.Angle(transform.up, _rot) > 80)
        {
            Vector3 carForward = transform.forward - Vector3.Dot(transform.forward, _rot) * _rot;
            transform.rotation = Quaternion.LookRotation(carForward);            
        }        
    }

    public void OrientToFloor()
    {
        if(carCollisionComponent.GetCollidedHit(-transform.up, out RaycastHit hit))
        {
            //Vector3 carUp = Vector3.Cross(transform.right, hit.normal);
            //transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            isGrounded = true;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation, Time.deltaTime* InAirfloorAlignSpeed);
            isGrounded = false;
        }
    }

    void Movement()
    {
        //Forward Acceleration
        //if (!drifting)
        //    sphere.AddForce(-kartModel.transform.right * currentSpeed, ForceMode.Acceleration);
        //else
        //    sphere.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        ////Gravity
        //sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        ////Steering
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);

        //RaycastHit hitOn;
        //RaycastHit hitNear;

        //Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitOn, 1.1f, layerMask);
        //Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitNear, 2.0f, layerMask);

        ////Normal Rotation
        //kartNormal.up = Vector3.Lerp(kartNormal.up, hitNear.normal, Time.deltaTime * 8.0f);
        //kartNormal.Rotate(0, transform.eulerAngles.y, 0);
    }

    void CarInputUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    float time = Time.timeScale == 1 ? .2f : 1;
        //    Time.timeScale = time;
        //}

        ////Follow Collider
        //transform.position = sphere.transform.position - new Vector3(0, 0.4f, 0);

        ////Accelerate
        //if (Input.GetButton("Fire1"))
        //    speed = acceleration;

        ////Steer
        //if (Input.GetAxis("Horizontal") != 0)
        //{
        //    int dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
        //    float amount = Mathf.Abs((Input.GetAxis("Horizontal")));
        //    Steer(dir, amount);
        //}

        ////Drift
        //if (Input.GetButtonDown("Jump") && !drifting && Input.GetAxis("Horizontal") != 0)
        //{
        //    drifting = true;
        //    driftDirection = Input.GetAxis("Horizontal") > 0 ? 1 : -1;

        //    foreach (ParticleSystem p in primaryParticles)
        //    {
        //        p.startColor = Color.clear;
        //        p.Play();
        //    }

        //    kartModel.parent.DOComplete();
        //    kartModel.parent.DOPunchPosition(transform.up * .2f, .3f, 5, 1);

        //}

        //if (drifting)
        //{
        //    float control = (driftDirection == 1) ? ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 0, 2) : ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 2, 0);
        //    float powerControl = (driftDirection == 1) ? ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, .2f, 1) : ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 1, .2f);
        //    Steer(driftDirection, control);
        //    driftPower += powerControl;

        //    ColorDrift();
        //}

        //if (Input.GetButtonUp("Jump") && drifting)
        //{
        //    Boost();
        //}

        //currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); speed = 0f;
        //currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;

        ////Animations    

        ////a) Kart
        //if (!drifting)
        //{
        //    kartModel.localEulerAngles = Vector3.Lerp(kartModel.localEulerAngles, new Vector3(0, 90 + (Input.GetAxis("Horizontal") * 15), kartModel.localEulerAngles.z), .2f);
        //}
        //else
        //{
        //    float control = (driftDirection == 1) ? ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, .5f, 2) : ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 2, .5f);
        //    kartModel.parent.localRotation = Quaternion.Euler(0, Mathf.LerpAngle(kartModel.parent.localEulerAngles.y, (control * 15) * driftDirection, .2f), 0);
        //}

        ////b) Wheels
        //frontWheels.localEulerAngles = new Vector3(0, (Input.GetAxis("Horizontal") * 15), frontWheels.localEulerAngles.z);
        //frontWheels.localEulerAngles += new Vector3(0, 0, sphere.velocity.magnitude / 2);
        //backWheels.localEulerAngles += new Vector3(0, 0, sphere.velocity.magnitude / 2);

        ////c) Steering Wheel
        //steeringWheel.localEulerAngles = new Vector3(-25, 90, ((Input.GetAxis("Horizontal") * 45)));

    }
}
