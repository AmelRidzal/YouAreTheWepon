using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayLevelOne()
    {
        SceneManager.LoadSceneAsync("Lvl1");
    }

    public void LoadMainMenu(){
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
