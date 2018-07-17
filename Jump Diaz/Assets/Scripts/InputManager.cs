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
    [SerializeField]
    Player player;
	
	void Update ()
    {
        // Player verification and Input handling
        if (player)
        {
            // Left or A Input
            if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)) { player.MovePlayerHorizontally(Vector2.left); }
            // Rigth or D Input
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) { player.MovePlayerHorizontally(Vector2.right); }
            // Up or Space Input
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) { player.StartAscent(); }
        }
	}

    public void AssignPlayer(Player p)
    {
        player = p;
    }
}
