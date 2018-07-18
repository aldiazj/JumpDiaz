using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // ------ Singleton
    private static InputManager instance = null;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(InputManager).ToString());
                    instance = go.AddComponent<InputManager>();
                }
            }
            return instance;
        }
    }

    // ------ Input receivers
    PlayerMovement playerMovement;
    Player player;
    GameManager gameManager;

    void Update ()
    {
        // Player verification and Input handling
        if (playerMovement)
        {
            // Left or A Input
            if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)) { playerMovement.MovePlayerHorizontally(Vector2.left); }
            // Rigth or D Input
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) { playerMovement.MovePlayerHorizontally(Vector2.right); }
            // Up or Space Input
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) { playerMovement.StartAscent(); }
        }
        if (player)
        {
            if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.State == GameStates.Gameover)
                player.ResestPlayer();
        }
        if (gameManager)
        {
            if (Input.GetKeyDown(KeyCode.Space) && gameManager.State == GameStates.Gameover)
                gameManager.ResetGame();
        }
    }

    public void AssignPlayerMovement(PlayerMovement p)
    {
        playerMovement = p;
    }
    public void AssignPlayer(Player p)
    {
        player = p;
    }
    public void AssignGameManager(GameManager gm)
    {
        gameManager = gm;
    }
}
