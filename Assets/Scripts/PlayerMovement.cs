using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform rightBounds, leftBounds;
    Vector2 goalPos;
    public float speed = 5f;
    void Start()
    {
        GetNewGoal();
    }

    void Update()
    {
        Vector3 dir = (Vector3)goalPos - transform.position;
        transform.position += Time.deltaTime * speed * dir.normalized;
        if (Vector2.Distance(transform.position, goalPos) <= .2f)
            GetNewGoal();
    }
    private void GetNewGoal()
    {
        goalPos = GameUtils.GetRandomPositionBetween(rightBounds.position, leftBounds.position);
    }
}
