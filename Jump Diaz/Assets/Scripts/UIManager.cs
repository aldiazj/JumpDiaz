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
    [SerializeField]
    Text retryText;

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
        highScoreText.text = "HI" + value.ToString("00000");
    }

    public void ModifyScore(int value)
    {
        scoreText.text = "SC" + value.ToString("00000");
    }
    public void ModifyRetryText(bool value)
    {
        retryText.enabled = value;
    }
}
