using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager instance;

    [SerializeField] Light light;

    [SerializeField] float rangeStart;
    [SerializeField] float rangeIncrease;
    [SerializeField] float rangeEnd;

    bool isGameStarted = false;

    float range;

    void Awake()
    {
        if (instance == null) instance = this;

        range = rangeStart;
        
    }

    void Update()
    {
        UpdateRange();
        UpdateLight();
    }

    void UpdateLight()
    {
        light.range = range;
    }

    void UpdateRange()
    {
        if (isGameStarted && range < rangeEnd)
        {
            range += rangeIncrease;
        }
        
    }

    public void StartGame()
    {
        isGameStarted = true;
    }
}
