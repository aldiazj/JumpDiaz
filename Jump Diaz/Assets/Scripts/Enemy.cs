using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingHazard 
{
    [SerializeField]
    SpriteRenderer enemyRenderer;

    private void Awake()
    {
        hazardTransform = GetComponent<Transform>();
        enemyRenderer = GetComponentInChildren<SpriteRenderer>();
        offset = 0.25f;
        HazardManager.Instance.ReceiveEnemy(this);
    }

    private void Update()
    {
        Move();
    }

    public override void Initialize(Vector2 dir)
    {
        base.Initialize(dir);
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        enemyRenderer.color = new Color(r,g,b);
        Vector2 newPos = hazardTransform.position;
        newPos.y += 0.25f;
        hazardTransform.position = newPos;
    }

    protected override bool CheckBoundaries()
    {
        return !LimitsManager.CharacterInsideLine(hazardTransform.position);
    }
}
