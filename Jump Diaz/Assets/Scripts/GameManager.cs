using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Play,
    Pause,
    levelTransition,
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
    [SerializeField]
    private int initialHoles = 1;
    [SerializeField]
    private int initialEnemies = 1;

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
                    ModifyScore(5);
                actualFloor = value;
            }
                
        }
    }

    int score;

	void Start () 
	{
        // Game setup
        SetupHoles();
        SetupEnemies();
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
            case GameStates.levelTransition:
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

    private void SetupEnemies()
    {
        // Send all the holes asked
        for (int i = 0; i < initialEnemies; i++)
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
}
