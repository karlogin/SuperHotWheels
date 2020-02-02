using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPiece : MonoBehaviour
{

    public void SetTrackPiece(Vector3 _pos)
    {
        transform.localPosition = _pos;
    }
    
}
