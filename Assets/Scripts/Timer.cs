using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    private float time = 0f;
    private bool isRun = false;

    private void Update()
    {
        if (isRun)
            time += Time.deltaTime;
    }

    public void StarTimer ()
    {
        time = 0f;
        isRun = true;
    }

    public void StopTimer()
    {
        isRun = false;        
    }

    public float GetTime ()
    {
        return (float)Math.Round(time,1) * 10;
    }
    
}
