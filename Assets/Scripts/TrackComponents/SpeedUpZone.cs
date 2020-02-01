using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpZone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        CarCollisionComponent carCollision = other.GetComponent<CarCollisionComponent>();
        if (carCollision)
        {
            carCollision.SetToMaxSpeed();
        }
    }
}
