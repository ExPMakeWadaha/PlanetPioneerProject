using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour {
    public void loadGameOverScene()
    {
        SceneManager.LoadScene("gameOverScene");
    }

    void Update()
    {
        if (BuildManager.getHuman() == 0)
            loadGameOverScene();
    }
}
