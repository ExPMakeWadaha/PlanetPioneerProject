using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
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
    
    
    public WholeGameData wholeGameData;
    //첫 게임 로딩에서 전체 게임 데이터를 가지고 게임매니저까지 보내줘야한다. jsonLoader에서 값을 받아올것이여
    public List<BuildingData> buildingDataList;
    //로딩을 첫로딩창에서 하기 위해 있다. jsonLoader에서 값을 받아올것이여
    public JsonManager jsonManager;     //로딩해준다
    
    public string gameStartTime;
    //게임시작시간을 알기위해 넣는다. 전나중요함
    public int gameTimer;       //초단위

    public GameObject dumyLoadingBar; // 로딩바로 쓸 큐브
    //증말 제일중요한거. 게임 시작을 하게되면 wholeGameData에서 lastPlayTime을 받아온다
    //이전에 접속한 시간을 얻어내면 그때부터 지금까지 시간을 알 수 있다
    //그런데 시간초를 게임매니저에서 세어주게 되면은 메뉴에서 서성이는 시간은 시간초로 안세어진느 것이다
    //게임을 꺼도 세어져야하는게 시간이다. 그렇기 때문에 게임을 켜자마자 시간을 재줘야한다
    //이거는 Time.realtimeSinceStartup이라는 읽기전용 변수가 있으니 일단은 고민해본다.

    public int pastSeconds;
    //지난게임으로부터 지금까지 지난 시간을 초로 나타냄

    int nowStage;


    //버튼이 클릭될 때 실행될 메서드 만들기
    public void LoadScene(string sceneName, int stageIndex)
    {
        SceneManager.LoadScene(sceneName);
        nowStage = stageIndex;
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
        jsonManager = new JsonManager();
        gameStartTime = System.DateTime.Now.ToString();
        WholeGameDataLoad();
        StartCoroutine(FirstSceneLoadCoroutine());
        nowStage = 0;
        pastSeconds = TimeSubtractionToSeconds(wholeGameData.lastPlayTime, gameStartTime);
        

        
    }

    //메뉴신을 불러오는 코루틴. 여기서 앵간한 로딩은 다 해줘야한다.
    IEnumerator FirstSceneLoadCoroutine()
    {
        //먼저 다른함수들이 돌 수 있게 리턴을 찍어준다
        yield return null;
        //씬로딩은 그다음에 실행한다
        AsyncOperation operation = SceneManager.LoadSceneAsync("MenuScene");
        //비동기로 씬 로딩하는거. 일케 안하면 게임이 멈춰버린다
        operation.allowSceneActivation = true;
        
        Vector3 vector = new Vector3(0, (operation.progress - 0.5f) * 5, 0);
        while (!operation.isDone)
        {
            vector.y = (operation.progress - 0.5f) * 10;
            dumyLoadingBar.transform.position = vector;
            //로딩바 채워지는걸 여기서 넣는다
            yield return null;
            
        }
    }

    IEnumerator JsonLoadCoroutine(){
        yield return null;
        
    }

    void WholeGameDataLoad()
    {
        wholeGameData = jsonManager.Load();
        buildingDataList = jsonManager.LoadBuildingData();
    }

    private void OnApplicationQuit()
    {
        wholeGameData.lastPlayTime = System.DateTime.Now.ToString();
        jsonManager.Save(wholeGameData);
        //일반종료시 끔
    }

    private void OnApplicationPause(bool pause)
    {
        //jsonManager.Save(wholeGameData);
        //강제종료시 끔
    }

    //time - time 기능 구현
    public int TimeSubtractionToSeconds(string pastTime, string latestTime)
    {
        DateTime past = DateTime.Parse(pastTime);
        DateTime latest = DateTime.Parse(latestTime);
        //string to datetime
        if(past ==null || latest == null)
        {
            Debug.Log("time is null");
            Application.Quit();
            return 0;
        }

        TimeSpan span = latest - past;
        int seconds = (int)span.TotalSeconds;
        Debug.Log("subtraction is " + seconds);

        return seconds;
    }
}
