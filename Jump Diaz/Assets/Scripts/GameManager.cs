using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Play,
    Pause,
    LevelTransition,
    Gameover
}

public class GameManager : MonoBehaviour 
{
    // ------ Singleton
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(GameManager).ToString());
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    // Actual state of the game
    GameStates state;
    public GameStates State { get { return state; } }

    // Holes to be set at the beggining of the level
    private int initialHoles = 2;


    // Actual floor where the playerMovement is, ONLY modify it on the Player class
    int actualFloor = 0;
    public int ActualFloor
    {
        get { return actualFloor; }
        set
        {
            if (value >= 0 && value <= Utils.MAX_NUMBER_OF_FLOORS)
            {
                if (value > actualFloor)
                {
                    ModifyScore(5*level);
                    HazardManager.Instance.SendHole();
                }
                actualFloor = value;
                if (actualFloor == Utils.MAX_NUMBER_OF_FLOORS)
                {
                    ChangeState(GameStates.LevelTransition);
                    SetLevelUp();
                }
            }
                
        }
    }

    int score;
    int level;

    private void Start()
    {
        InputManager.Instance.AssignGameManager(this);
        SetLevelUp();
    }

    void SetLevelUp () 
	{
        // Game setup
        level++;
        FindObjectOfType<PlayerMovement>().ResetPLayerPosition();
        HazardManager.Instance.ResetLevel();
        SetupHoles();
        SetupEnemies(level-1);
        ChangeState(GameStates.Play);
    }

    /// <summary>
    /// Change the actual state of the game
    /// </summary>
    /// <param name="newState"> New state of the game</param>
    public void ChangeState(GameStates newState)
    {
        // the state will be changed only if the change is done from a legal state
        bool isChangeAccepted = false;
        switch (state)
        {
            case GameStates.Play:
                isChangeAccepted = true;
                break;
            case GameStates.Pause:
            case GameStates.LevelTransition:
            case GameStates.Gameover:
                if (newState == GameStates.Play)
                    isChangeAccepted = true;
                break;
        }
        if (isChangeAccepted)
        {
            state = newState;
        }
    }

    private void SetupHoles()
    {
        // Send all the holes asked
        for (int i = 0; i < initialHoles; i++)
        {
            HazardManager.Instance.SendHole();
        }
    }

    private void SetupEnemies(int enemyQuantity)
    {
        // Send all the holes asked
        for (int i = 0; i < enemyQuantity; i++)
        {
            HazardManager.Instance.SendEnemy();
        }
    }

    private void ModifyScore(int value)
    {
        // Add up score variable ant then show it on the UI
        score += value;
        UIManager.Instance.ModifyScore(score);
    }

    public void ResetGame()
    {
        UIManager.Instance.ModifyRetryText(false);
        UIManager.Instance.ModifyHighScore(score);
        score = 0;
        UIManager.Instance.ModifyScore(score);
        SetLevelUp();
        ChangeState(GameStates.Play);
    }
}
