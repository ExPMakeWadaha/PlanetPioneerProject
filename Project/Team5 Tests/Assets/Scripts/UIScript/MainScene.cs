using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1080, 1920, true);

        StartCoroutine(LoadCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool creditLoaded = false;
    public void CreditButton()
    {
        creditLoaded = true;
        SceneManager.LoadScene("CreditScene");
    }
    


    IEnumerator LoadCoroutine()
    {
        yield return new WaitForSeconds(3f);
        if(!creditLoaded)
            SceneManager.LoadScene("StartScene");
    }
}
