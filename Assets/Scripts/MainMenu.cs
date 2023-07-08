using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Slider _musicSlider, _vfxSlider, _masterSlider;

    private void Awake()
    {
        AudioManager.Instance.Play("MainMenuMusic");
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
    public void Quit ()
    {
        Application.Quit();
    }

    #region SoundController
    public void MusicVolume ()
    {
        
    }

    #endregion

}
