using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils
{
    public static Vector2 GetRandomPositionBetween(Vector2 pos1, Vector2 pos2)
    {
        float newXvalue = UnityEngine.Random.Range(pos1.x, pos2.x);
        return new Vector2(newXvalue, pos1.y);
    }
    public static bool CheckIfBallHitPlayer(Vector3 playerThatBallIsGoingTo, Vector3 playerOther, Vector3 ball)
    {
        var distanceBetweenOtherPlayerAndBall = Vector3.Distance(ball, playerOther);
        var distanceBetweenPlayers = Vector3.Distance(playerThatBallIsGoingTo, playerOther);
        if (distanceBetweenOtherPlayerAndBall > distanceBetweenPlayers) //ball passed through other player
        {
            return true;
        }
        return false;
    }
}
