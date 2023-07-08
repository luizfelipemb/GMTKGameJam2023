using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void Quit ()
    {
        Application.Quit();
    }

}
