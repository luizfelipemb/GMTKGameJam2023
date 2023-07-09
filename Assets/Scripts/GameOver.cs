using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	private bool isSomethingPlaying = false;
    private void Start()
    {
		Scene currentScene = SceneManager.GetActiveScene();

		string sceneName = currentScene.name;

		if (sceneName == "GameOver")
		{
			if(AudioManager.Instance.IsSomethingPlaying())
            {
				AudioManager.Instance.StopAudio("MainMenuMusic");
            }
			AudioManager.Instance.Play("GameOver");
		}
		else if (sceneName == "Victory")
		{
			if (AudioManager.Instance.IsSomethingPlaying())
			{
				AudioManager.Instance.StopAudio("MainMenuMusic");
			}
			AudioManager.Instance.Play("Victory");
		}
	}

    public void Quit()
    {
        Application.Quit();
    }
}
