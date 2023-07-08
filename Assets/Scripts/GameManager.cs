using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool _isGamePaused = false;
    private bool _tutorialWasPlayed = true;

    public bool GetTutorialWasPlayed { get => _tutorialWasPlayed; }
    public bool SetTutorialWasPlayed { set => _tutorialWasPlayed = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Carrega a booleana salva quando o jogo é iniciado
        _tutorialWasPlayed = PlayerPrefs.GetInt("_tutorialWasPlayed", 0) == 1;
    }

    public void TutorialVisited ()
    {
        PlayerPrefs.SetInt("tutorialWasPlayed", _tutorialWasPlayed ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        // Exemplo de como pausar ou despausar o jogo pressionando a tecla 'P'
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = _isGamePaused ? 0f : 1f;
        }
    }


}
