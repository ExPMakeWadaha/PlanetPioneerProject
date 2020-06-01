using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {




    public int nowMoney;        //현재 가지고 있는 돈
public GameObject cube;     //큐브,Prefab폴더에 있는 Cube를 스크립트 Inspector로 끌어오면 코드 내에서의 초기화 없이 사용가능하다
    public GameObject sphere;   //구, 상동
    //public으로 지정한 객체와 변수는 이런식으로 Unity창에서 초기화 할 수 있다. 자세한 내용은 Unity Public변수 초기화를 검색하자
    //Prefab이 뭐냐고 물을텐데, Prefab만드는 법도 구글링하자
    GameObject newCube;
    GameObject newSphere;
    int nowBuildingIndex;       //현재 빌딩의 개수
    float timer;                //타이머,
    List<Building> buildingList;    //빌딩들을 동적할당 할 수 있는 동적배열(C++ 에서의 array *)
                                    //C#에서의 동적배열은 Array가 아닌 List로 한다.
                                    //포인터 개념을 사용하지 않기 때문에 C++에서의 동적배열 방법은 사용하지 않는다.
                                    //대신 이미 있는 라이브러리인 List를 써준다.List가 궁금하면 API를 찾아보길 바란다.
    bool isOptioning;   //환경설정창이 켜져있는지 판정하는 bool값
    //켜져있으면 true, 꺼지면 false

    //200528 19:37이 아래부터 상훈이가 적은겁니다 for data saving TEST
    /*
    WholeGameData dumyGameData; 
    StageData stageData;
    JsonManager jsonManager;
    public List<BuildingData> jsonBuildingList;*/


    //OptionManager에서 부르는 함수이다.
    public void Optioning()
    {
        isOptioning = !isOptioning;
        //값을 토글시켜준다. Optioning이 아닐 때 
    }

    void Start()
    {

    }

    void Update()   //60Hz모니터를 사용중이라면 1초에 60번 실행되는 함수다. 144Hz모니터라면 144번 실행된다
    {
        //예제2번에서 환경설정이 켜질 때 timer는 정지하여야 한다ㅣ.


        /*
        if (isOptioning==false)
        {
            
            timer += Time.deltaTime;    //timer는 시계다. 1초가 올라갈 때마다 1씩 올라가야 하는데, 이걸 재어주는 변수가 Time.deltaTime이다
                                        //1프레임이 지나가는 떄에 걸리는 실제 시간이 deltaTime이다.
                                        //모니터가 60Hz라고 가정하면, Time.deltaTime은 60분의 1초가 된다

            if (Input.GetKeyDown(KeyCode.A))
            {
                // ...............여기다가 작성하세용........…
                //a가 눌릴때 작동되는 함수
                newCube = Instantiate(cube);    //큐브를 월드에 생성하고 newCube로 지정한다
                Building b2 = new Building(1, nowBuildingIndex++, 1, newCube);
                //Start와 마찬가지로 지역변수 b2를 만들고, 리스트에 Add해준다.
                buildingList.Add(b2);
                Debug.Log("f1");
                //Debug.Log는 cout이나 printf정도로 생각하면 된다
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                // ...............여기다가 작성하세용........…
                //b가 눌릴때 작동되는 함수
                Debug.Log("f2");
                newSphere = Instantiate(sphere);
                Building b3 = new Building(1, nowBuildingIndex++, 1, newSphere);
                //Start와 마찬가지로 지역변수 b3를 만들고, 리스트에 Add해준다.
                buildingList.Add(b3);
            }
            if (timer >= 2)
            {
                timer = 0;
                //현재 배열에 들어가있는 빌딩들의 income을 모두 더해야 한다.
                //C++이라면 array의 크기를 재서 for int를 하던가, index만큼 진행하였을 것이다.
                //C#의 List에는 List.Count를 통해 List의 크기를 알 수 있다.
                //이를 통해 for구문을 몇번 반복할지 알 수 있다. 그래서 아래대로 작성하면
                for (int i = 0; i < buildingList.Count; i++)
                {
                    nowMoney += buildingList[i].IncomeCollect();
                }
                Debug.Log("Now Money is " + nowMoney);
                //Debug.Log는 cout이나 printf정도로 생각하면 된다
            }


            /*
            //200528 19:37이 아래부터 상훈이가 적은겁니다 for data saving
            if (Input.GetKeyDown(KeyCode.F1))
            {
                stageData.buildingList = buildingList;      //this is justa test. never do like this
                dumyGameData.ChangeStageData(stageData, 0);
                jsonManager.Save(dumyGameData);
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                dumyGameData = jsonManager.Load();
                foreach (Building building in buildingList)
                {
                    Destroy(building.buildingObject);
                }
                buildingList = dumyGameData.stageArray[0].buildingList;
                foreach (Building building in buildingList)
                {
                    Instantiate(cube);
                    cube.transform.position = building.positionVector;
                }
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                jsonBuildingList = jsonManager.LoadBuildingData();
                foreach(BuildingData a in jsonBuildingList)
                {
                    Debug.Log(a.buildingName);
                }
            }
        }
       */
    }

    //...............여기다가 작성하세용...........

}


