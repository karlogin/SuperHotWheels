using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappablePoint : MonoBehaviour
{
    [SerializeField]
    Transform parentObjectToMove;
    public int snapLayer;
    public float radiusToCheck = 1;

    SnappablePoint currentNearestPoint;

    private void Start()
    {
        snapLayer = LayerMask.GetMask("SnapPoints");
    }

    public void SnapToNearestSnappablePoint()
    {
        CheckForNearbySnapPoints();
        if (currentNearestPoint)
        {
            SnapToPoint();
        }
    }

    void SnapToPoint()
    {       
        //Difference between wanted snap point and direction
        Vector3 currentPointAndParentDif = currentNearestPoint.transform.position - currentNearestPoint.GetParent().position;
        Debug.DrawLine(currentNearestPoint.transform.position, currentNearestPoint.transform.forward, Color.green);

        //rotate this object parent
        Quaternion difference = parentObjectToMove.rotation * Quaternion.Inverse(transform.rotation);
        parentObjectToMove.rotation = Quaternion.LookRotation(-currentNearestPoint.transform.forward, transform.up) * difference;

        //Get difference between this snap point and parent
        Vector3 pointAndParentDif = parentObjectToMove.position - transform.position;
        Debug.DrawLine(transform.position, transform.forward, Color.blue);

        //Move parent to position
        parentObjectToMove.position = currentNearestPoint.transform.position + pointAndParentDif;

        //Remove currentNearestPoint
        currentNearestPoint = null;
    }

    void CheckForNearbySnapPoints()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, radiusToCheck, snapLayer);
        //Debug.Log(colliderArray.Length);
        for(int i = 0; i < colliderArray.Length; i++)
        {
            SnappablePoint point = colliderArray[i].GetComponent<SnappablePoint>();
            if (point)
            {
                if(point == this)
                {
                    //Empty so you don't make the nearestPoint = this
                }
                else if(point.GetParent() == this.parentObjectToMove)
                {
                    //Empty so you don't make the nearestPoint = sister SnappablePoint
                }
                else if(currentNearestPoint == null)
                {
                    currentNearestPoint = point;
                }
                else
                {
                    currentNearestPoint = GetNearestSnapPoint(currentNearestPoint, point);
                }
            }
        }
    }

    SnappablePoint GetNearestSnapPoint(SnappablePoint _pointA, SnappablePoint _pointB)
    {
        float distA = (_pointA.transform.position - transform.position).magnitude;
        float distB = (_pointB.transform.position - transform.position).magnitude;
        if(distA > distB)
        {
            return _pointA;
        }
        else
        {
            return _pointB;
        }
    }

    public Transform GetParent()
    {
        return parentObjectToMove;
    }
}
