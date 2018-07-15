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
    [SerializeField]
    bool isFalling = false;
    [SerializeField]
    bool isAscending = false;
    [SerializeField]
    bool ableToPass = false;

    private void OnEnable()
    {
        // The Input manager needs to know which one is the valid instance of the player, so whenever a player is activated it should assign itself as the current player
        InputManager.Instance.AssignPlayer(this);
    }

    private void Awake()
    {
        rgbdy = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // If no ground detection, keep falling
        if (isFalling)
        {
            Fall(); 
        }
        // If
        if (isAscending)
        {
            Ascend();
        }
    }

    private void Fall()
    {
        // Move down the player at a given fall speed
        rgbdy.MovePosition(rgbdy.position + Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void Ascend()
    {
        // Move down the player at a given fall speed
        rgbdy.MovePosition(rgbdy.position + Vector2.up * fallSpeed * Time.deltaTime);
    }

    public void StartAscent()
    {
        if (!isFalling)
        {
            isAscending = true;
        }
    }

    /// <summary>
    /// Moves the player to certain direction
    /// </summary>
    /// <param name="direction"> Direction pointing where the player is going to move, it is suggested to use Vector2.left or Vector2.right</param>
    public void MovePlayerHorizontally(Vector2 direction)
    {
        if (isFalling || isAscending)
            return;
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
        if (collision.otherCollider == feetCollider && collision.gameObject.CompareTag(Utils.TAG_FLOOR))
        {
            isFalling = false;
            ableToPass = false;
        }
        // 
        if (collision.otherCollider == headCollider && collision.gameObject.CompareTag(Utils.TAG_FLOOR) && !ableToPass )
        {
            Debug.Log("startFalling");
            isFalling = true;
            isAscending = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player touches a hole with the feet and it is not ascending, then he will fall
        if (collision.CompareTag(Utils.TAG_HOLE) && collision.IsTouching(feetCollider) && !isAscending)
        {
            Debug.Log("fall");
            isFalling = true;
        }
        // If the player touches a hole with the head and is not falling then he will pass to next floor
        if (collision.CompareTag(Utils.TAG_HOLE) && collision.IsTouching(headCollider) && !isFalling)
        {
            ableToPass = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the player touches a hole with the feet and it is ascending, then he will stop ascending
        if (collision.CompareTag(Utils.TAG_HOLE) && !collision.IsTouching(feetCollider) && isAscending)
        {
            Debug.Log("startFalling exit");
            collision.GetComponent<Hole>().LetPlayerPass();
            isAscending = false;
            isFalling = true;
        }
    }
}
