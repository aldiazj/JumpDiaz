﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    // ------ Singleton
    private static HoleManager instance = null;

    public static HoleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HoleManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(HoleManager).ToString());
                    instance = go.AddComponent<HoleManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    List<Hole> availableHoles = new List<Hole>();
	
    public void SendHole()
    {
        int x = Random.Range(0, availableHoles.Count);
        Vector2 holeDirection = (Random.Range(0, 2) == 0) ? Vector2.left : Vector2.right;
        availableHoles[x].Initialize(holeDirection);
        availableHoles.RemoveAt(x);
    }

    /// <summary>
    /// Adds the hole given as argument, to the available pool of holes
    /// </summary>
    /// <param name="hole"></param>
    public void ReceiveHole(Hole hole)
    {
        availableHoles.Add(hole);
    }
}