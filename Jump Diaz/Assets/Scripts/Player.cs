using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]  
public class Player : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 3.0f;
    [SerializeField]
    float fallSpeed = 3.0f;
    [SerializeField]
    Rigidbody2D rgbdy;
    [SerializeField]
    CircleCollider2D headCollider;
    [SerializeField]
    CircleCollider2D feetCollider;
    bool isFalling = true;

    private void Awake()
    {
        InputManager.Instance.AssignPlayer(this);
        rgbdy = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Fall();
    }

    private void Fall()
    {
        if (isFalling)
        {
            rgbdy.MovePosition(rgbdy.position + Vector2.down * fallSpeed * Time.deltaTime);
        }
    }
    /// <summary>
    /// Moves the player to certain direction
    /// </summary>
    /// <param name="direction"> Direction pointing where the player is going to move, it is suggested to use Vector2.left or Vector2.right</param>
    public void MovePlayerHorizontally(Vector2 direction)
    {
        // Pop up an error if vertical movement is attempted
        if (direction.y != 0)
        {
            Debug.LogError("No  verical movement is allowed in this method");
            return;
        }
        // Move the player in a given horizintal direction multiplied by the movement speed and the delta time
        rgbdy.MovePosition(rgbdy.position+direction*movementSpeed*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player touches the floor and set isFalling to false
        if (collision.otherCollider == feetCollider && collision.gameObject.CompareTag("Floor"))
        {
            isFalling = false;
        }
    }
}
