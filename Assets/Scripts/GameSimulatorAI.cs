using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSimulatorAI : MonoBehaviour
{
    public static GameSimulatorAI Instance { get; private set; }

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

    public float SuspectometerDownAmount = .1f;
    private float ballspeed = 5f;
    private bool startsTheRound;
    private bool ballGoingTo;

    public int p1Points;
    public int p2Points;
    public int p1Sets;
    public int p2Sets;

    [HideInInspector]
    public bool BallNearLine;
    [HideInInspector]
    public bool BallNearNet;
    [HideInInspector]
    public bool BallNearPlayer;

    public Action ActionScoreChanged;
    public Action ActionBallMissed;
    public Action ActionBallBackToGame;

    [HideInInspector]
    public bool BallGoingOut { private set; get; }
    private float goingOutTimer;
    public float TimeBallGoesOut;
    private Vector3 missedDirection;
    private bool playerThatMissed;
    [HideInInspector]
    public float SuspectometerAmount { private set; get; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        startsTheRound = PLAYER1;
        StartNewRound(true);
    }

    void Update()
    {
        BallMovement();
        if (!BallGoingOut && BallCanChangeDirection())
        {
            ballGoingTo = !ballGoingTo;
            ballspeed = UnityEngine.Random.Range(BallMinSpeed, BallMaxSpeed);
            if (BallMissed(ballGoingTo))
            {
                ActionBallMissed?.Invoke();
                BallGoingOut = true;
                missedDirection = PlayerBoolToTransform(ballGoingTo).position - ball.transform.position;
            }
        }

        if (BallGoingOut)
        {
            goingOutTimer += Time.deltaTime;
            if (goingOutTimer > TimeBallGoesOut)
            {
                BallGoingOut = false;
                goingOutTimer = 0;
                StartNewRound(false);
            }
                
        }

        if (SuspectometerAmount > 0f)
            SuspectometerAmount -= SuspectometerDownAmount * Time.deltaTime;
    }
    public void BallOutButton()
    {
        if (BallNearLine) //button at the right time
            IncreaseSuspectometer(0.10f);
        else
            IncreaseSuspectometer(0.30f);
        BallMissedByButton();
    }
    public void HitTheBodyButton()
    {
        if (BallNearPlayer) //button at the right time
            IncreaseSuspectometer(0.10f);
        else
            IncreaseSuspectometer(0.30f);
        BallMissedByButton();
    }
    public void BallOnNetButton()
    {
        if (BallNearNet) //button at the right time
            IncreaseSuspectometer(0.10f);
        else
            IncreaseSuspectometer(0.30f);
        BallMissedByButton();
    }
    private void IncreaseSuspectometer(float value)
    {
        SuspectometerAmount += value;
        if (SuspectometerAmount >= 1f)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
    public void BallMissedByButton()
    {
        playerThatMissed = PLAYER2;
        BallGoingOut = true;
        ActionBallMissed?.Invoke();
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

        ActionBallBackToGame?.Invoke();
        ballspeed = UnityEngine.Random.Range(BallMinSpeed, BallMaxSpeed);
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
            startsTheRound = !startsTheRound;
        }
        ActionScoreChanged?.Invoke();
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
        if (BallGoingOut)
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
