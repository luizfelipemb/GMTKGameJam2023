using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSimulatorAI : MonoBehaviour
{
    enum Player
    {
        p1 = 1,
        p2 = 2
    }
    [Header("Assignables")]
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    Transform P1BoundsLeft, P1BoundsRight, P2BoundsLeft, P2BoundsRight;

    [Header("Settings")]
    public float BallMinSpeed;
    public float BallMaxSpeed;

    private float ballspeed = 5f;
    private Player startsTheRound;
    private Player ballGoingTo;

    void Start()
    {
        startsTheRound = Player.p1;
        StartRoundPositionings(startsTheRound);
    }

    void Update()
    {
        BallMovement();
        CheckIfBallChangedDirection();
    }
    private void CheckIfBallChangedDirection()
    {
        if (ballGoingTo == Player.p1 && GameUtils.CheckIfBallHitPlayer(player1.transform.position, player2.transform.position,ball.transform.position))
        {
            ballGoingTo = Player.p2;
            ballspeed = UnityEngine.Random.Range(BallMinSpeed,BallMaxSpeed);
        }
        else if (ballGoingTo == Player.p2 && GameUtils.CheckIfBallHitPlayer(player2.transform.position, player1.transform.position, ball.transform.position))
        {
            ballGoingTo = Player.p1;
            ballspeed = UnityEngine.Random.Range(BallMinSpeed, BallMaxSpeed);
        }
    }
    private void StartRoundPositionings(Player whostarts)
    {
        switch (whostarts)
        {
            case Player.p1:
                player1.transform.position = GameUtils.GetRandomPositionBetween(P1BoundsLeft.position, P1BoundsRight.position);
                player2.transform.position = GameUtils.GetRandomPositionBetween(P2BoundsLeft.position, P2BoundsRight.position);
                ball.transform.position = player1.transform.position;
                ballGoingTo = Player.p2;
                break;
            case Player.p2:
                player1.transform.position = GameUtils.GetRandomPositionBetween(P1BoundsLeft.position, P1BoundsRight.position);
                player2.transform.position = GameUtils.GetRandomPositionBetween(P2BoundsLeft.position, P2BoundsRight.position);
                ball.transform.position = player2.transform.position;
                ballGoingTo = Player.p1;
                break;
        }
    }
    private void BallMovement()
    {
        if (ballGoingTo == Player.p1)
        {
            Vector3 dir = player1.transform.position - ball.transform.position;
            ball.transform.position += Time.deltaTime * ballspeed * dir.normalized ;
        }
        else if (ballGoingTo == Player.p2)
        {
            Vector3 dir = player2.transform.position - ball.transform.position;
            ball.transform.position += Time.deltaTime * ballspeed * dir.normalized;
        }
    }
}
