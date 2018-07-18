using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Running,
    Stunned
}

[RequireComponent(typeof(Rigidbody2D))]  
public class Player : MonoBehaviour
{
    [SerializeField]
    int lives = 6;
    PlayerState state;
    public PlayerState State { get { return state; } }

    private void Awake()
    {
        InputManager.Instance.AssignPlayer(this);
    }

    public void DecreaseLife()
    {
        // If there is at least one life decrease its number in one unit
        if (lives > 0)
            lives--;
        if (lives == 0)
            Die(); // Else, the playerMovement has no more lives and must die
        UIManager.Instance.ModifyLives(lives);
    }

    private void Die()
    {
        // Order the GameManager to change the actual state to GameOver
        GameManager.Instance.ChangeState(GameStates.Gameover);
        UIManager.Instance.ModifyRetryText(true);
    }

    public void StartStunState()
    {
        StartCoroutine(GetStunned());
    }

    WaitForSeconds stunnedWait = new WaitForSeconds(3f);

    IEnumerator GetStunned()
    {
        state = PlayerState.Stunned;
        yield return stunnedWait;
        state = PlayerState.Running;
    }

    public void ResestPlayer()
    {
        lives = 6;
        UIManager.Instance.ModifyLives(lives);
    }
}
