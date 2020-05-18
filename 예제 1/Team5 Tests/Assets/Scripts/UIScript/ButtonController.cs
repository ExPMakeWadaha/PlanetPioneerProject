using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    //이거는 버그때문에 만들었다
    //StartScene에서 GamePlayScene을 갔다가 옵션창을 통해서 다시 StartScene으로 돌아오면 GameStartButton이 작동을 안한다.
    //작동을 안하는 이유는, Scene을 다시 로딩했을 때 DontDestroyOnLoad를 통해 살아남은 SceneLoader가 있어서
    //새로 Scene을 켰을 때 있는 SceneLoade가 Destroy되버린 것이다. GameStartButton은 DontDestroyOnLoad로 살아남은 SceneLoader를
    //찾을 수가 없다. 그래서, ButtonController로 SceneLoader를 잡아줘야하는 수고스러움을 해ㅑㅇ 하는 것이다. 

    SceneLoader sceneLoader;
    void Start()
    {
        //이거는 ScneneLoader랑 이 스크립트중 어느놈의 Start가 먼저 실행되는지에 따라 값이 달라져서 예외처리하려고 이런거다.
        //참고로 Start는 Scene에서 위쪽에 있는 오브젝트들의 스크립트부터 차례대로 실행된다.
        if(SceneLoader.singleTone == null)
        {
            sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        }
        else
        {
            sceneLoader = SceneLoader.singleTone;
        }
            

    }

    public void PlaySceneLoad()
    {
        sceneLoader.LoadScene("GamePlayScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
