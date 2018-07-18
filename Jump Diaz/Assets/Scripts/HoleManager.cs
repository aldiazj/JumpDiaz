using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    // ------ Singleton
    private static HazardManager instance = null;

    public static HazardManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HazardManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(HazardManager).ToString());
                    instance = go.AddComponent<HazardManager>();
                }
            }
            return instance;
        }
    }

    List<Hole> availableHoles = new List<Hole>();
    List<Enemy> availableEnemies = new List<Enemy>();

    /// <summary>
    /// Send a hole with a random direction, if there is any available at the pool
    /// </summary>
    public void SendHole()
    {
        if (availableHoles.Count > 0)
        {
            int x = Random.Range(0, availableHoles.Count);
            Vector2 holeDirection = (Random.Range(0, 2) == 0) ? Vector2.left : Vector2.right;
            availableHoles[x].Initialize(holeDirection);
            availableHoles.RemoveAt(x); 
        }
    }

    /// <summary>
    /// Send a hole with a random direction, if there is any available at the pool
    /// </summary>
    public void SendEnemy()
    {
        if (availableEnemies.Count > 0)
        {
            int x = Random.Range(0, availableEnemies.Count);
            Vector2 enemyDirection = (Random.Range(0, 2) == 0) ? Vector2.left : Vector2.right;
            availableEnemies[x].Initialize(enemyDirection);
            availableEnemies.RemoveAt(x);
        }
    }

    /// <summary>
    /// Adds the hole given as argument, to the available pool of holes
    /// </summary>
    /// <param name="hole"></param>
    public void ReceiveHole(Hole hole)
    {
        availableHoles.Add(hole);
    }

    /// <summary>
    /// Adds the hole given as argument, to the available pool of holes
    /// </summary>
    /// <param name="hole"></param>
    public void ReceiveEnemy(Enemy enemy)
    {
        availableEnemies.Add(enemy);
    }
}
