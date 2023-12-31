using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField][Range(0.15f, 0.9f)] private float _OutOfMoment;
    [SerializeField][Range(0.15f, 0.9f)] private float _RightMoment;
    private float _timer;
    private float _maxTime = 10.0f;
    [SerializeField] private TextMeshProUGUI _p1Points;
    [SerializeField] private TextMeshProUGUI _p2Points;

    [SerializeField]
    private Image _suspectometerFill;

    [SerializeField] private List<Button> actionButtons;

    private void OnEnable()
    {
        _suspectometerFill.fillAmount = 0f;
    }
    private void Start()
    {
        GameSimulatorAI.Instance.ActionBallHit += OnBallHit;
        GameSimulatorAI.Instance.ActionScoreChanged += OnScoreChanged;
        GameSimulatorAI.Instance.ActionBallMissed += DisableActionButtons;
        GameSimulatorAI.Instance.ActionBallBackToGame += EnableActionButtons;

        if (AudioManager.Instance.IsPlaying("GameOver"))
            AudioManager.Instance.StopAudio("GameOver");
        else if (AudioManager.Instance.IsPlaying("Victory"))
            AudioManager.Instance.StopAudio("Victory");

        AudioManager.Instance.Play("MainMenuMusic");
    }
    private void OnDisable()
    {
        GameSimulatorAI.Instance.ActionBallHit -= OnBallHit;
        GameSimulatorAI.Instance.ActionScoreChanged -= OnScoreChanged;
        GameSimulatorAI.Instance.ActionBallMissed -= DisableActionButtons;
        GameSimulatorAI.Instance.ActionBallBackToGame -= EnableActionButtons;
    }
    private void OnBallHit()
    {
        AudioManager.Instance.Play("BallHit"+UnityEngine.Random.Range(1,5));
    }
    private void OnScoreChanged()
    {
        string p1UIpoints = PointsConverterToTenis(GameSimulatorAI.Instance.p1Points, GameSimulatorAI.Instance.p2Points);
        string p2UIpoints = PointsConverterToTenis(GameSimulatorAI.Instance.p2Points, GameSimulatorAI.Instance.p1Points);

        _p1Points.text = $"{GameSimulatorAI.Instance.p1Sets}-{p1UIpoints}";
        _p2Points.text = $"{GameSimulatorAI.Instance.p2Sets}-{p2UIpoints}";
    }
    private void Update()
    {
        _suspectometerFill.fillAmount = GameSimulatorAI.Instance.SuspectometerAmount;
    }
    private void DisableActionButtons()
    {
        foreach (Button button in actionButtons)
        {
            Animator buttonAnimator = button.GetComponent<Animator>();
            buttonAnimator.SetTrigger(button.animationTriggers.disabledTrigger);
        }
    }
    private void EnableActionButtons()
    {
        foreach (Button button in actionButtons)
        {
            Animator buttonAnimator = button.GetComponent<Animator>();
            buttonAnimator.SetTrigger(button.animationTriggers.normalTrigger);
            //button.animationTriggers.normalTrigger = true;
        }
    }
    private string PointsConverterToTenis(int points, int advPoints)
    {
        if (points == 0)
            return "0";
        else if (points == 1)
            return "15";
        else if (points == 2)
            return "30";
        else if (points == 3)
            return "40";
        else
        {
            if (points - advPoints == 1)
                return "adv";
            else
                return "40";
        }
    }

    public void BallOut ()
    {
        
        GameSimulatorAI.Instance.BallOutButton();
    }

    public void HitTheBody ()
    {
        GameSimulatorAI.Instance.HitTheBodyButton();
    }

    public void BallOnNet ()
    {
        GameSimulatorAI.Instance.BallOnNetButton();
    }
}
