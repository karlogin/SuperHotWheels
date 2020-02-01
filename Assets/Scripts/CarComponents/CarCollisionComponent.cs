using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarCollisionComponent : MonoBehaviour
{
    Rigidbody rb;
    CarObject carObject;
    [Header("Required Components")]
    [SerializeField]
    Transform forwardRayPoint;
    [Header("Collision Parameters")]
    [SerializeField]
    float rayLength = 1; //Does not scale

    /// <summary>
    /// Intended for initialization by CarObject script
    /// </summary>
    /// <param name="_carObject"></param>
    public void InitCarCollision(CarObject _carObject)
    {
        rb = GetComponent<Rigidbody>();
        carObject = _carObject;
    }

    /// <summary>
    /// Give the total of direction and wanted "speed" to push the object
    /// </summary>
    /// <param name="_movement"></param>
    public void AddMovementForce(Vector3 _movement)
    {
        rb.AddForce(_movement);
    }

    /// <summary>
    /// Get current velocity of attached rigidbody
    /// </summary>
    /// <returns></returns>
    public float GetCurrentSpeed()
    {
        return rb.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        HandleForwardCollision();
    }

    void HandleForwardCollision()
    {        
        if(GetCollidedHit(carObject.transform.forward, out RaycastHit hit))
        {
            carObject.SetCarForwardRotation(hit.normal);
        }
    }

    /// <summary>
    /// Essentially Physics.Raycast but with ray being drawn and the position already given.
    /// </summary>
    /// <param name="_direction"></param>
    /// <param name="_hit"></param>
    /// <returns></returns>
    public bool GetCollidedHit(Vector3 _direction, out RaycastHit _hit)
    {
        Ray ray = new Ray(forwardRayPoint.position, _direction);
        if(Physics.Raycast(ray, out _hit, rayLength))
        {
            //Debug.Log(_hit.transform.gameObject);
            Debug.DrawRay(ray.origin, ray.direction, Color.red, rayLength);
            return true;
        }
        return false;
    }
}
