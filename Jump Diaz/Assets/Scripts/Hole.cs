﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    Collider2D col;
    Transform holeTransform;

    bool isPlaced;
    public static float passingTime = 0;
    Vector2 direction = Vector2.right;

    [SerializeField]
    float movingSpeed = 3.0f;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        holeTransform = GetComponent<Transform>();
        HoleManager.Instance.ReceiveHole(this);
    }

    private void Update()
    {
        if (passingTime > 0)
        {
            passingTime -= 1 * Time.deltaTime;
            col.enabled = false;
        }
        else if (!col.enabled)
        {
            col.enabled = true;
        }
        if (isPlaced)
        {
            holeTransform.Translate(direction * movingSpeed * Time.deltaTime);
            if (!LimitsManager.IsVisible(Camera.main, holeTransform.position))
                MoveToNextLine();
        }
    }

    public void Initialize(Vector2 dir)
    {
        direction = dir;
        Vector2 newPos = holeTransform.position;
        newPos.y = UnityEngine.Random.Range(-3, 5);
        newPos.x = UnityEngine.Random.Range(-5.8f , 5.8f);
        holeTransform.position = newPos;
        isPlaced = true;
    }

    private void MoveToNextLine()
    {
        Vector2 newPos = holeTransform.position;
        newPos.y += (direction == Vector2.right) ? -1 : 1;
        newPos.x *= -1;
        if (newPos.y < -3 || newPos.y > 4)
        {
            isPlaced = false;
            newPos = new Vector2(100, 100);
        }
        holeTransform.position = newPos;
        //holeTransform.
    }

    public void LetPlayerPass()
    {
        passingTime = 2;
    }
}