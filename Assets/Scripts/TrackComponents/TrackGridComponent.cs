using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGridComponent : MonoBehaviour
{
    float gridUnit = 0.025f;
    List<TrackPiece> trackList;

    // Start is called before the first frame update
    void Start()
    {
        GetCurrentTrackPieces();
    }

    void GetCurrentTrackPieces()
    {
        trackList = new List<TrackPiece>();
        trackList.AddRange(FindObjectsOfType<TrackPiece>());
    }


}
