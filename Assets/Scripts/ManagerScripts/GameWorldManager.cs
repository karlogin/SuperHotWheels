using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldManager : MonoBehaviour
{
    public static GameWorldManager Instance;
    public float timeScale = 1;
    public CarObject car;
    public float secondsToCheck = 1;
    public float timeTilGo = 10;

    private void Awake()
    {
        Instance = this;
    }

    IEnumerator TimerToGo()
    {
        yield return new WaitForSeconds(timeTilGo);
        car.SetIsGo(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
        StartCoroutine(CheckForCarOutOfBounds());
        StartCoroutine(TimerToGo());
    }

    IEnumerator CheckForCarOutOfBounds()
    {
        while (gameObject)
        {
            if(car.transform.position.y < 0)
            {
                car.ResetCar();
                StartCoroutine(TimerToGo());
            }
            yield return new WaitForSeconds(secondsToCheck);
        }
    }
}
