using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
public class Clock
{
    public Action pass; 
    public float rate;
    public int count;
    private MonoBehaviour mono;
    private bool run = false;
    public Clock(float rate)
    {
        this.rate = rate;
        mono = Object.FindObjectOfType<MonoBehaviour>();
    }

    public void start()
    {
        run = true;
        mono.StartCoroutine(tick());
    }

    public void stop()
    {
        run = false;
        count = 0;
    }

    IEnumerator tick()
    {
        while (run)
        {
            pass?.Invoke();
            count++;
            yield return new WaitForSeconds(rate);
        }
    }
}

