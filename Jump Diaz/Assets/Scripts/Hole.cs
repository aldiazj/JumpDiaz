using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MovingHazard
{
    Collider2D col;
    public static float passingTime = 0;    

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        hazardTransform = GetComponent<Transform>();
        HazardManager.Instance.ReceiveHole(this);
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
        Move();
    }

    public void LetPlayerPass()
    {
        passingTime = 2;
    }

    protected override bool CheckBoundaries()
    {
        return !LimitsManager.HoleInsideLine(Camera.main, hazardTransform.position);
    }
}
