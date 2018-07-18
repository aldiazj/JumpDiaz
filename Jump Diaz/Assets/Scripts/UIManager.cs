using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    // ------ Singleton
    private static UIManager instance = null;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(UIManager).ToString());
                    instance = go.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    Image[] livesImages;
    [SerializeField]
    Text highScoreText;
    [SerializeField]
    Text scoreText;

    public void ModifyLives(int value)
    {
        for (int i = 0; i < livesImages.Length; i++)
        {
            livesImages[i].enabled = false;
        }
        for (int i = 0; i < value; i++)
        {
            livesImages[i].enabled = true;
        }
    }

    public void ModifyHighScore(int value)
    {
        highScoreText.text = "Hi" + value.ToString("00000");
    }

    public void ModifyScore(int value)
    {
        highScoreText.text = "Hi" + value.ToString("00000");
    }
}
