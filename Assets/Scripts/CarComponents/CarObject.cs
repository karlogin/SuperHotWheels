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
    Transform startPiece;
    [SerializeField]
    Vector3 startPoint;
    [SerializeField]
    Quaternion startRotation;
    [SerializeField]
    float forwardSpeed = 1;
    [SerializeField]
    float maxSpeed = 1;
    [SerializeField]
    float InAirfloorAlignSpeed = 1;

    bool isGrounded = false;
    public bool isGo = false;

    private void Start()
    {
        carCollisionComponent.InitCarCollision(this);
        if (startPiece)
        {
            SetStartingTransform(startPiece);
        }
        else
        {
            SetStartingTransform(transform.position, transform.rotation);
        }        
    }

    public void SetIsGo(bool _isGo)
    {
        isGo = _isGo;
    }

    public void SetStartingTransform(Vector3 _pos, Quaternion _rot)
    {
        startPoint = _pos;
        startRotation = _rot;
    }

    public void SetStartingTransform(Transform _transform )
    {
        startPoint = _transform.position;
        startRotation = _transform.rotation;
    }

    public void ResetCar()
    {
        isGo = false;
        if (startPiece)
        {
            carCollisionComponent.transform.position = startPiece.position;
            transform.rotation = startPiece.rotation;
        }
        else
        {
            carCollisionComponent.transform.position = startPoint;
            transform.rotation = startRotation;
        }
        
        carCollisionComponent.ResetVelocity();
    }

    void FixedUpdate()
    {
        HandleCarMovement();
        OrientToFloor();
    }

    void HandleCarMovement()
    {
        if (isGrounded && isGo)
        {
            if(carCollisionComponent.GetCurrentSpeed() < maxSpeed)
            {
                carCollisionComponent.AddMovementForce(transform.forward * forwardSpeed);
            }            
        }        
    }

    public float GetMaxSpeed()
    {
        return forwardSpeed;
    }

    public float GetCurrentSpeed()
    {
        return carCollisionComponent.GetCurrentSpeed();
    }

    public float DEBUGAngleDifference;
    /// <summary>
    /// Instantly rotates the transform of the car to align it's facing direction to a collider 
    /// that is too steep to drive on.
    /// </summary>
    /// <param name="_rot"></param>
    public void SetCarForwardRotation(Vector3 _rot)
    {
        //Only set rotation if is a wall
        DEBUGAngleDifference = Vector3.Angle(transform.up, _rot);
        if (Vector3.Angle(transform.up, _rot) > 80)
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
