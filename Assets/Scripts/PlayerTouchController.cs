using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerInput))]
public class PlayerTouchController : MonoBehaviour
{
    Vector2 lastTouchPoint, currentPoint;
    PlayerInput input;
    TrackPiece currentGrabbed;

    [Header("Required Properties")]
    public Camera cam;
    ////public Satellite launchObject;
    //public LineRenderer launchRender;
    //public float launchSpeedMultiplier = 1;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        ClickScreen();       
    }

    public Vector2 GetScreenTouchPoint()
    {
        return cam.ScreenToWorldPoint(input.GetClickPosition());
    }

    /// <summary>
    /// Intended as an Update-able function, check the mouse button(0) input status and 
    /// </summary>
    public void ClickScreen()
    {
        if (input.GetPlayerClickDown())
        {
            //LaunchSatelliteStart();
            FindGrabableObject(GetScreenTouchPoint());
        }        
        if (input.GetPlayerClick())
        {
            //LaunchSatelliteUpdate();
            DragGrabable(GetScreenTouchPoint());

        }
        else if (input.GetPlayerClickUp())
        {
            //LaunchSatelliteEnd();
            DropGrabable();
        }
    }

    void FindGrabableObject(Vector3 _worldPoint)
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            TrackPiece g = hit.transform.GetComponent<TrackPiece>();
            TrackPiece parent = hit.transform.GetComponentInParent<TrackPiece>();
            if (g)
            {
                currentGrabbed = g;
            }
            else if (parent)
            {
                currentGrabbed = parent;
            }
        }
    }

    void DragGrabable(Vector3 _worldPoint)
    {
        if (currentGrabbed)
        {
            Vector3 point = cam.ScreenToWorldPoint(Input.mousePosition);
            currentGrabbed.SetTrackPieceGlobal(point);
        }
    }

    void DropGrabable()
    {
        if (currentGrabbed)
        {
            currentGrabbed.DropPiece();
            currentGrabbed = null;
        }
    }
}
