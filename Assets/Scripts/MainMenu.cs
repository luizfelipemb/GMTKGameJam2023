using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

public class MainMenu : MonoBehaviour
{
    private bool _tutorialWasPlayed = false;

    [SerializeField] private GameObject LoadTutorial;
    [SerializeField] private GameObject LoadGameplay;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (AudioManager.Instance.IsPlaying("GameOver"))
            AudioManager.Instance.StopAudio("GameOver");
        else if (AudioManager.Instance.IsPlaying("Victory"))
            AudioManager.Instance.StopAudio("Victory");

        AudioManager.Instance.Play("MainMenuMusic");
    }

    public void PlayTutorial()
    {
        if (GameManager.Instance.GetTutorialWasPlayed)
        {
            LoadGameplay.GetComponent<MMFeedbacks>().PlayFeedbacks();
            return;
        }

        LoadTutorial.GetComponent<MMFeedbacks>().PlayFeedbacks();
        GameManager.Instance.SetTutorialWasPlayed = true;
        GameManager.Instance.TutorialVisited();
    }

    #region Links
    public void StartGame ()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void LucasMorgado ()
    {
        Application.OpenURL("https://lucasmorgado.itch.io");
    }

    public void LuizFelipe ()
    {
        Application.OpenURL("https://luizfelipemb.itch.io");
    }

    public void Mounirtohami ()
    {
        Application.OpenURL("https://mounirtohami.itch.io");
    }
    #endregion

    #region Quit
    public void Quit ()
    {
        Application.Quit();
    }
    #endregion


}
