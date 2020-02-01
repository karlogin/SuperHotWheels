using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarCollisionComponent : MonoBehaviour
{
    Rigidbody rb;
    CarObject carObject;
    public float rayLength = 1;

    public void InitCarCollision(CarObject _carObject)
    {
        rb = GetComponent<Rigidbody>();
        carObject = _carObject;
    }

    public void AddMovementForce(Vector3 _movement)
    {
        rb.AddForce(_movement);
    }

    void HandleForwardCollision()
    {        
        if(GetCollidedHit(carObject.transform.forward, out RaycastHit hit))
        {
            carObject.SetCarForwardRotation(hit.normal);
        }
    }

    public bool GetCollidedHit(Vector3 _direction, out RaycastHit _hit)
    {
        Ray ray = new Ray(transform.position, _direction);
        if(Physics.Raycast(ray, out _hit, rayLength))
        {
            //Debug.Log(_hit.transform.gameObject);
            Debug.DrawRay(ray.origin, ray.direction, Color.red, rayLength);
            return true;
        }
        return false;
    }

    private void OnCollisionStay(Collision collision)
    {
        HandleForwardCollision();
    }
}
