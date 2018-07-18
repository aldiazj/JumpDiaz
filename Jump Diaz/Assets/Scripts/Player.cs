using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Running,
    Falling,
    Stunned
}

[RequireComponent(typeof(Rigidbody2D))]  
public class Player : MonoBehaviour
{
    [SerializeField]
    int lives = 6;
    PlayerState state;
    public PlayerState State { get { return state; } }

    public void DecreaseLife()
    {
        // If there is at least one life decrease its number in one unit
        if (lives > 1)
            lives--;
        else
            Die(); // Else, the playerMovement has no more lives and must die
        UIManager.Instance.ModifyLives(lives);
    }

    private void Die()
    {
        // Order the GameManager to change the actual state to GameOver
        GameManager.Instance.ChangeState(GameStates.Gameover);
        // TODO give a feedbak to the playerMovement that he lost
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

}
