using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField]
    float movementSpeed = 3.0f;
    [SerializeField]
    float fallSpeed = 3.0f;

    [SerializeField]
    CircleCollider2D headCollider;
    [SerializeField]
    CircleCollider2D feetCollider;

    Rigidbody2D rgbdy;
    Player player;

    bool isFalling = false;
    bool isAscending = false;
    bool ableToPass = false;


    private void OnEnable()
    {
        // The Input manager needs to know which one is the valid instance of the playerMovement, so whenever a playerMovement is activated it should assign itself as the current playerMovement
        InputManager.Instance.AssignPlayerMovement(this);
    }

    private void Awake()
    {
        rgbdy = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
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
        // Move down the playerMovement at a given fall speed
        rgbdy.MovePosition(rgbdy.position + Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void Ascend()
    {
        // Move down the playerMovement at a given fall speed
        rgbdy.MovePosition(rgbdy.position + Vector2.up * fallSpeed * Time.deltaTime);
    }

    public void StartAscent()
    {
        // if the playerMovement is not falling and actual state of the game is in play, then the playerMovement can jump
        if (!isAscending && !isFalling && GameManager.Instance.State == GameStates.Play)
            isAscending = true;
    }

    /// <summary>
    /// Moves the playerMovement to certain direction
    /// </summary>
    /// <param name="direction"> Direction pointing where the playerMovement is going to move, it is suggested to use Vector2.left or Vector2.right</param>
    public void MovePlayerHorizontally(Vector2 direction)
    {
        if (isFalling || isAscending || GameManager.Instance.State != GameStates.Play)
            return;
        // Pop up an error if vertical movement is attempted
        if (direction.y != 0)
        {
            Debug.LogError("No  verical movement is allowed in this method");
            return;
        }
        // Move the playerMovement in a given horizintal direction multiplied by the movement speed and the delta time
        rgbdy.MovePosition(rgbdy.position + direction * movementSpeed * Time.deltaTime);
        if (!LimitsManager.PlayerInsideLine(rgbdy.position))
            MoveToOtherSide();
    }

    private void MoveToOtherSide()
    {
        // If the hole is moving right then at th end of the line it should go down one level, else it should go up
        Vector2 newPos = rgbdy.position;
        newPos.x *= -1;
        // The hole needs to be repositioned on x to avoid conflicts with the LimitsManager
        if (newPos.x < 0)
            newPos.x += 0.1f;
        if (newPos.x > 0)
            newPos.x -= 0.1f;
        // If the hole reaches the top, it should start again at the bottom and viceversa
        rgbdy.MovePosition(newPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the playerMovement touches the floor with the feet and set isFalling to false
        if (collision.otherCollider == feetCollider && collision.gameObject.CompareTag(Utils.TAG_FLOOR))
        {
            isFalling = false;
            ableToPass = false;
            int floorNumber = (int)collision.gameObject.GetComponent<Transform>().position.y + Utils.FLOORS_OFFSET;
            GameManager.Instance.ActualFloor = floorNumber;
            if (GameManager.Instance.ActualFloor == 0)
            {
                player.DecreaseLife();
            }
        }
        // 
        if (collision.otherCollider == headCollider && collision.gameObject.CompareTag(Utils.TAG_FLOOR) && !ableToPass)
        {
            isFalling = true;
            isAscending = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the playerMovement touches a hole with the feet and it is not ascending, then he will fall
        if (collision.CompareTag(Utils.TAG_HOLE) && collision.IsTouching(feetCollider) && !isAscending)
        {
            isFalling = true;
        }
        // If the playerMovement touches a hole with the head and is not falling then he will pass to next floor
        if (collision.CompareTag(Utils.TAG_HOLE) && collision.IsTouching(headCollider) && !isFalling)
        {
            ableToPass = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the playerMovement touches a hole with the feet and it is ascending, then he will stop ascending
        if (collision.CompareTag(Utils.TAG_HOLE) && !collision.IsTouching(feetCollider) && isAscending)
        {
            collision.GetComponent<Hole>().LetPlayerPass();
            isAscending = false;
            isFalling = true;
        }
    }
}
