using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarCollisionComponent : MonoBehaviour
{
    Rigidbody rb;
    public CarObject carObject;
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
        GetCollidedHit();
    }

    void GetCollidedHit()
    {
        Ray ray = new Ray(transform.position, carObject.transform.forward);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray, out hit, rayLength))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red, rayLength);
            carObject.SetCarRotation(hit.normal);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleForwardCollision();
    }
}
