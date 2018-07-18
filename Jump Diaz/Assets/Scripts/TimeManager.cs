using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // ------ Singleton
    private static TimeManager instance = null;

    public static TimeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TimeManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(TimeManager).ToString());
                    instance = go.AddComponent<TimeManager>();
                }
            }
            return instance;
        }
    }

    float enviromentTimer = 1.0f;
    public float EnviromentTimer { get { return enviromentTimer; } }

    public void ActivateSlowMo()
    {
        StartCoroutine(FreezeTime());
    }

    WaitForSeconds freezedSeconds = new WaitForSeconds(0.2f);
    WaitForSeconds fastSeconds = new WaitForSeconds(0.1f);

    IEnumerator FreezeTime()
    {
        enviromentTimer = 0.1f;
        yield return freezedSeconds;
        enviromentTimer = 2.0f;
        yield return fastSeconds;
        enviromentTimer = 1.0f;
    }
}
