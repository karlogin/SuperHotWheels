using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldManager : MonoBehaviour
{
    public static GameWorldManager Instance;
    public float timeScale = 1;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
