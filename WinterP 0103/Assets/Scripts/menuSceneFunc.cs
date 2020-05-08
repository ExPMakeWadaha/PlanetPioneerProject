using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuSceneFunc : MonoBehaviour {
    public void loadSratringScene()
    {
        SceneManager.LoadScene("startingScene");
    }
    public void quitGame()
    {
        Application.Quit();
    }

}
