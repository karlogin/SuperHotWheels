using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObject : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField]
    CarCollisionComponent carCollisionComponent;

    [Header("Car Properties")]
    //10 > seems to be enough to get over 45 degree angles
    //15 does ramp off
    //20 Can do larger loop-de-loop
    [SerializeField]
    float forwardSpeed = 1;
    [SerializeField]
    float maxSpeed = 1;
    [SerializeField]
    float InAirfloorAlignSpeed = 1;

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
            if(carCollisionComponent.GetCurrentSpeed() < maxSpeed)
            {
                carCollisionComponent.AddMovementForce(transform.forward * forwardSpeed);
            }            
        }        
    }

    /// <summary>
    /// Instantly rotates the transform of the car to align it's facing direction to a collider 
    /// that is too steep to drive on.
    /// </summary>
    /// <param name="_rot"></param>
    public void SetCarForwardRotation(Vector3 _rot)
    {
        //Only set rotation if is a wall
        if(Vector3.Angle(transform.up, _rot) > 80)
        {
            Vector3 carForward = transform.forward - Vector3.Dot(transform.forward, _rot) * _rot;
            transform.rotation = Quaternion.LookRotation(carForward);            
        }        
    }

    void OrientToFloor()
    {
        if(carCollisionComponent.GetCollidedHit(-transform.up, out RaycastHit hit))
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            isGrounded = true;
        }
        else
        {
            //using slerp in this instance to prevent it from trying to align to unintended "ground"
            //plus looks cool
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation, Time.deltaTime* InAirfloorAlignSpeed);
            isGrounded = false;
        }
    }    
}
