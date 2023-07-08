using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuryAnimator : MonoBehaviour
{
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite handsUp;
    private SpriteRenderer thisSprite;

    private void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        GameSimulatorAI.Instance.ActionButtonClicked += Fault;
        GameSimulatorAI.Instance.ActionBallBackToGame += BackToGame;
    }

    private void Fault()
    {
        thisSprite.sprite = handsUp;
        AudioManager.Instance.Play("Whistle");
    }
    private void BackToGame()
    {
        thisSprite.sprite = normal;
    }
}
