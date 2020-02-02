using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGridComponent : MonoBehaviour
{
    float gridUnit = 0.025f;
    List<TrackPiece> trackList;
    public TrackPiece firstTrack;
    public TrackPiece endTrack;


    // Start is called before the first frame update
    void Start()
    {
        GetCurrentTrackPieces();
    }

    public void InitTrack(CarObject _car)
    {
        _car.SetStartingTransform(firstTrack.StartTransform.transform);
        _car.ResetCar();
    }

    void GetCurrentTrackPieces()
    {
        trackList = new List<TrackPiece>();
        trackList.AddRange(FindObjectsOfType<TrackPiece>());
    }


}
