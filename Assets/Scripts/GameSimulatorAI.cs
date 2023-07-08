using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSimulatorAI : MonoBehaviour
{
    const bool PLAYER1 = true;
    const bool PLAYER2 = false;

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

    [Range(0,100)]
    public int P1ChanceOfMissing;
    [Range(0, 100)]
    public int P2ChanceOfMissing;

    private float ballspeed = 5f;
    private bool startsTheRound;
    private bool ballGoingTo;

    private int p1Points;
    private int p2Points;
    private int p1Sets;
    private int p2Sets;

    private bool ballGoingOut;
    private float goingOutTimer;
    public float TimeBallGoesOut;
    private Vector3 missedDirection;
    private bool playerThatMissed;


    void Start()
    {
        startsTheRound = PLAYER1;
        StartNewRound(true);
    }

    void Update()
    {
        BallMovement();
        if (!ballGoingOut && BallCanChangeDirection())
        {
            ballGoingTo = !ballGoingTo;
            ballspeed = UnityEngine.Random.Range(BallMinSpeed, BallMaxSpeed);
            if (BallMissed(ballGoingTo))
            {
                Debug.Log("ball missed");
                ballGoingOut = true;
                missedDirection = PlayerBoolToTransform(ballGoingTo).position - ball.transform.position;
            }
        }

        if (ballGoingOut)
        {
            goingOutTimer += Time.deltaTime;
            if (goingOutTimer > TimeBallGoesOut)
            {
                ballGoingOut = false;
                goingOutTimer = 0;
                StartNewRound(false);
            }
                
        }
    }
    public void BallMissedByButton()
    {
        playerThatMissed = PLAYER2;
        ballGoingOut = true;
        missedDirection = PlayerBoolToTransform(ballGoingTo).position - ball.transform.position;
    }
    private bool BallMissed(bool ballgoingto)
    {
        var randomNumber = UnityEngine.Random.Range(0, 100);
        switch (ballgoingto)
        {
            case PLAYER1:
                if (randomNumber < P1ChanceOfMissing)
                {
                    playerThatMissed = PLAYER1;
                    return true;
                }
                break;
            case PLAYER2:
                if (randomNumber < P2ChanceOfMissing)
                {
                    playerThatMissed = PLAYER2;
                    return true;
                }
                break;
        }
        return false;
    }
    private Transform PlayerBoolToTransform(bool playerbool)
    {
        switch (playerbool)
        {
            case PLAYER1:
                    return player1.transform;
            case PLAYER2:
                    return player2.transform;
        }
    }
    private bool BallCanChangeDirection()
    {
        if (ballGoingTo == PLAYER1 && GameUtils.CheckIfBallHitPlayer(player1.transform.position, player2.transform.position,ball.transform.position))
        {
            return true;
        }
        else if (ballGoingTo == PLAYER2 && GameUtils.CheckIfBallHitPlayer(player2.transform.position, player1.transform.position, ball.transform.position))
        {
            return true;
        }
        return false;
    }
    private void StartNewRound(bool firstRound)
    {
        if(!firstRound)
            AssignPoints();

        ballspeed = UnityEngine.Random.Range(BallMinSpeed, BallMaxSpeed);
        startsTheRound = !startsTheRound;
        switch (startsTheRound)
        {
            case PLAYER1:
                player1.transform.position = GameUtils.GetRandomPositionBetween(P1BoundsLeft.position, P1BoundsRight.position);
                player2.transform.position = GameUtils.GetRandomPositionBetween(P2BoundsLeft.position, P2BoundsRight.position);
                ball.transform.position = player1.transform.position;
                ballGoingTo = PLAYER2;
                break;
            case PLAYER2:
                player1.transform.position = GameUtils.GetRandomPositionBetween(P1BoundsLeft.position, P1BoundsRight.position);
                player2.transform.position = GameUtils.GetRandomPositionBetween(P2BoundsLeft.position, P2BoundsRight.position);
                ball.transform.position = player2.transform.position;
                ballGoingTo = PLAYER1;
                break;
        }
    }
    private void AssignPoints()
    {
        if (playerThatMissed == PLAYER1)
        {
            p2Points++;
        }
        else
            p1Points++;
        if (SomeoneWinSet())
        {
            p1Points = 0;
            p2Points = 0;
        }
    }
    private bool SomeoneWinSet()
    {
        if (p1Points==4 && p2Points < 3)
        {
            p1Sets++;
            return true;
        }
        else if(p2Points == 4 && p1Points < 3)
        {
            p2Sets++; 
            return true;
        }
        else if(p1Points>=3 && p2Points>=3) //both 40 or adv points
        {
            if (p1Points - p2Points >= 2)
            {
                p1Sets++; 
                return true;
            }
            else if(p2Points - p1Points >= 2)
            {
                p2Sets++; 
                return true;
            }
        }
        return false;
    }
    private void BallMovement()
    {
        Vector3 direction;
        if (ballGoingOut)
            direction = missedDirection;
        else if (ballGoingTo == PLAYER1)
        {
            direction = player1.transform.position - ball.transform.position;
        }
        else
        {
            direction = player2.transform.position - ball.transform.position;
            
        }
        ball.transform.position += Time.deltaTime * ballspeed * direction.normalized;
    }
}
