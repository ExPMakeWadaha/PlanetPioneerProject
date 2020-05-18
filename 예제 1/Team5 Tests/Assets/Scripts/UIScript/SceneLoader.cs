using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader singleTone;
    //만약 SceneLoader가 실수로 두 개가 생기면 어떨까. 코드가 이리저리 꼬이고 렉도 걸릴것이다.
    //SingleTone은 두 개가 생기면 안되는 객체에 넣어주는 코딩방식이다.
    //Start에서 최초에 singleTone에 값을 넣는데, 만약 이미 다른 객체가 만들어져서 값이 만들어져있다면 자신을 삭제하게 한다.
    //오류를 방지하는 것이기 때문에 굳이 사용하지 않아도 상관은 없다. 이해가 안된다면 지금은 그냥 넘어가두 된다


    //버튼이 클릭될 때 실행될 메서드 만들기
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //scene로드는 scene의 이름을 string으로 호출하여 로드한다.
        //scene로드를 하려면 Editor에서 File/BuildSettings/ScenesInBuild에 로드하려는 Scene이 들어가있어야한다.
        //BuildSettings에 Scene이 들어가있지 않으면, 그 Scene을 사용하지 않겠다는 걸로 간주해서 로드를 할 수가 없다.
        //매개변수를 이용해서 신을 로드할것이다.

    }
    void Start()
    {
        if(singleTone == null)
        {
            singleTone = this;
            //만약 싱글톤이 배정되어있지 않다면 this객체를 할당해준다.
            
            DontDestroyOnLoad(gameObject);
            //SceneLoader를 이용하여 씬을 로딩 할거니까 씬이 로딩될 때 사라지지 않게 만들어준다.
        }
        else
        {
            Destroy(gameObject);
            //만약 싱글톤에 다른 값이 있다면, 이미 객체가 있다는 뜻이니까 이 스크립트가 달린 오브젝트를 삭제한다.
            //gameObject는 이 스크립트가 달려있는 게임오브젝트값이다.
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
