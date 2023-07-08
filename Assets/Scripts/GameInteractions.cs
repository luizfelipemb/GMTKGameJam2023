using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInteractions : MonoBehaviour
{
    private static GameInteractions _instance;
    public static GameInteractions Instance {get { return _instance;} private set { } }

    public bool BallNearLine;
    public bool BallNearNet;
    public bool BallNearPlayer;
    private GameSimulatorAI gameSimulatorAI;

    private void Awake()
    {
        _instance = this;
        gameSimulatorAI = GetComponent<GameSimulatorAI>();
    }
    public void UIButtonClicked()
    {
        gameSimulatorAI.BallMissedByButton();
    }
}
