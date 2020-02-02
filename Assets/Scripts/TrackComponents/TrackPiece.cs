using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPiece : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

/*    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }


    private Vector3 GetMouseAsWorldPoint()
    {

        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

    private void OnMouseUp()
    {
        DropPiece();
    }*/

    public void SetTrackPieceLocal(Vector3 _pos)
    {
        transform.localPosition = _pos;
    }

    public void SetTrackPieceGlobal(Vector3 _pos)
    {
        transform.position = new Vector3(_pos.x, transform.position.y, _pos.z);
    }

    public void DropPiece()
    {
        SnappablePoint[] pointArray = GetComponentsInChildren<SnappablePoint>();
        for(int i = 0; i < pointArray.Length; i++)
        {
            pointArray[i].SnapToNearestSnappablePoint();
        }
    }
    
}
