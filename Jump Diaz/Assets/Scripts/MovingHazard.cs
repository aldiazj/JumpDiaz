using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard : MonoBehaviour 
{
    protected bool isPlaced;
    protected Transform hazardTransform;

    Vector2 direction = Vector2.right;
    protected Vector2 initialPos;

    [SerializeField]
    float movingSpeed = 3.0f;
    protected float offset;

    public void Move()
    {
        if (isPlaced && GameManager.Instance.State == GameStates.Play)
        {
            hazardTransform.Translate(direction * movingSpeed * Time.deltaTime * TimeManager.Instance.EnviromentTimer);
            if (CheckBoundaries())
                MoveToNextLine();
        }
    }

    protected virtual bool CheckBoundaries()
    {
        return false;
    }

    public virtual void Initialize(Vector2 dir)
    {
        // Start a new hole at a random position and with a given direction
        direction = dir;
        Vector2 newPos = hazardTransform.position;
        newPos.y = UnityEngine.Random.Range(-3, 5);
        newPos.x = UnityEngine.Random.Range(-5.8f, 5.8f);
        hazardTransform.position = newPos;
        isPlaced = true;
    }

    protected void MoveToNextLine()
    {
        // If the hole is moving right then at th end of the line it should go down one level, else it should go up
        Vector2 newPos = hazardTransform.position;
        newPos.y += (direction == Vector2.right) ? -1 : 1;
        newPos.x *= -1;
        // The hole needs to be repositioned on x to avoid conflicts with the LimitsManager
        if (newPos.x < 0)
            newPos.x += 0.1f;
        if (newPos.x > 0)
            newPos.x -= 0.1f;
        // If the hole reaches the top, it should start again at the bottom and viceversa
        if (newPos.y < -3)
            newPos.y = 4 + offset;
        else if (newPos.y > 4 + offset)
            newPos.y = -3 + offset;
        hazardTransform.position = newPos;
    }

    public void ResetHazardPosition()
    {
        hazardTransform.position = initialPos;
    }
}
