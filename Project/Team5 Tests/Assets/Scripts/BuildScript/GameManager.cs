﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GameManager : MonoBehaviour {



    SceneLoader sceneLoader;    //여기서 정보를 다 땡겨오는거다.
    StageData stageData;    //현재 스테이지에서 가지고 있을 데이터. 
    int nowStage;    //현재 스테이지. 0 1 2 이다
    public int coin;        //현재 가지고 있는 돈
    public int mileage;        //마일리지
    
    //public으로 지정한 객체와 변수는 이런식으로 Unity창에서 초기화 할 수 있다. 자세한 내용은 Unity Public변수 초기화를 검색하자
    //Prefab이 뭐냐고 물을텐데, Prefab만드는 법도 구글링하자

    int nowBuildingIndex;       //현재 빌딩의 개수
    public int coinTimer;                  //코인을 얻는거의 타이머,
    public int coinIncomeSum;              //10초마다 얻게 될 코인의 합
    [SerializeField]
    List<Building> buildingList;    //빌딩들을 동적할당 할 수 있는 동적배열(C++ 에서의 array *)
                                    //C#에서의 동적배열은 Array가 아닌 List로 한다.
                                    //포인터 개념을 사용하지 않기 때문에 C++에서의 동적배열 방법은 사용하지 않는다.
                                    //대신 이미 있는 라이브러리인 List를 써준다.List가 궁금하면 API를 찾아보길 바란다.
    [SerializeField]
    List<BuildingData> buildingDataList;    //빌딩데이터 리스트 가져온다.
    bool isOptioning;   //환경설정창이 켜져있는지 판정하는 bool값
    List<int> incompletedIndexList; //미완성된 빌딩의 인덱스값들이다.

    List<string> boughtBuildingList;    //내가 건물을 사놓고 안지었을 때, 그 건물을 샀는지에 대한 유무
    public OptionManager optionManager; //

    const string objectTag = "Building";  //빌딩에 넣을 태그. Unity기능이니 잘 찾아보시요.
    //태그는 RayScript에서 RayCast당한 오브젝트의 태그가 "Building"일 때만 타겟으로 지정할 수 있게 만들었다.

    //OptionManager에서 부르는 함수이다.
    public void Optioning()
    {
        isOptioning = !isOptioning;

    }

    void Start()
    {
        sceneLoader = SceneLoader.singleTone;   //스태틱변수는 클래스로 바로 접근가능.
        nowStage = sceneLoader.nowStage;        //그냥 쓰기 편하려고 게임매니저로 가져옴
        stageData = sceneLoader.wholeGameData.stageArray[nowStage]; //스테이지 데이터 가져오고
        buildingList = stageData.buildingList;
        buildingDataList = sceneLoader.buildingDataListArray[nowStage];
        incompletedIndexList = stageData.incompletedIndex;
        coin = sceneLoader.wholeGameData.coin;
        mileage = stageData.mileage;
        //이 위까지는 그냥 stageData에서 받아온거다. 굳이 변수를 하나 더 만들어준 이유는
        //쓰기 편하라고이다.

        boughtBuildingList = stageData.boughtBuilidng;
        if(boughtBuildingList == null)
        {
            boughtBuildingList = new List<string>();
        }
        //단순한 버그처리용. 디버깅중에 이게 nullReferenceException이 자주 떠서 그렇다.

        coinIncomeSum = 0;  //초기화. 빌딩 지어질 떄마다 더해줄거. 초깃값은 onStageLoaded에서 정한다.
        coinTimer = 0;

        SetSceneLoader();
        //코인하고 마일리지도 가져오고

        //모든 데이터는 씬매니저를 통해 가져온다

        //1스테이지인데 빌딩이 아무것도 없다는 뜻이면 랜드마크가 없다는거다.
        if (nowStage == 0 && buildingList.Count == 0)
        {
            BuildStage1LandMark();
          //랜드마크 지어주낟
        }

        

        nowBuildingIndex = buildingList.Count;
        OnStageLoaded();
        //현재 개수 몇개인지 세어준다.
        StartCoroutine(BuildCoroutine());
        //빌드 코루틴에서 1초마다 체크를 해서 빌딩을 지어준다.
    }

    void Update()  
    {
        //무작위로 생성하는거. 디버그용임
        if (Input.GetKeyDown(KeyCode.A))
        {
            BuildRadomBuilding();
        }
    }


    //stage가 맨처음에 로딩되었을 때 불러줄 함수.
    void OnStageLoaded()
    {
        //지어진 빌딩이 뭔지 확인해서 짓는다.
        foreach(Building building in buildingList)
        {
            BuildingLoad(building);
            coinIncomeSum += building.GetData().incomeCoin;
        }
    }

    //Building객체를 새로 만들어주어야 하는경우, 로딩해서 짓는게 아니라, 실제로 게임내에서 사서 짓는경우
    void BuildStart(BuildingData data)
    {

        Building building = new Building(data, nowBuildingIndex);
        buildingList.Add(building);
        Debug.Log(building.buildingName);

        incompletedIndexList.Add(nowBuildingIndex);
        nowBuildingIndex++;

        //빌딩 인덱스 현재거를 넣어준 다음에!!!! 1을 더해주어야한다
        //빌딩이 0개면 인덱스도 0이다. 인덱스는 1부터시작이 아니라 0부터시작이니까 0넣어주고 1더해줘야해!!
        building.isCompleted = false;

        building.buildingObject =Instantiate(data.incompletedPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        building.buildingObject.tag = objectTag;
        BoxCollider collider = building.buildingObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(data.width,1, data.height);
        //빌딩오브젝트를 만들어주고, 콜라이더를 씌워서 클릭할 수 있게 만들어준다
        //콜라이더의 싸이즈는 빌딩에 맞게 가로 세로를 넣어주낟.
    }

    //이미 StageData에서 Building객체가 존재하기 때문에 새롭게 Building을 만들어 줄 필요가 없을경우
    void BuildStart(Building building)
    {
        //만약 로딩해서 불러온 거면은 새로운 객체를 만들어줄 필요가 없다.
        //isLoaded가 true면은 이미 buildingList에 있는 놈인거다.

        BuildingData data = building.GetData();
        
        //빌딩 인덱스 현재거를 넣어준 다음에!!!! 1을 더해주어야한다
        //빌딩이 0개면 인덱스도 0이다. 인덱스는 1부터시작이 아니라 0부터시작이니까 0넣어주고 1더해줘야해!!

        building.buildingObject = Instantiate(data.incompletedPrefab, building.positionVector, Quaternion.identity);
        building.buildingObject.tag = objectTag;
        BoxCollider collider = building.buildingObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(data.width, 1, data.height);
        //빌딩오브젝트를 만들어주고, 콜라이더를 씌워서 클릭할 수 있게 만들어준다
        //콜라이더를 안만들어주면은 rayCast가 안먹어요
        //콜라이더의 싸이즈는 빌딩에 맞게 가로 세로를 넣어주낟.
    }

    void BuildingLoad(Building building)
    {
        Debug.Log(building.buildingName);
        
        //지나간 시간이 얼마나 되는지 확인.
        BuildingData buildingData = FindBuildingData(building.buildingName) ;   //처음에 자동으로 널값이 들어가질 않는다.
        //짓고 난 다음 시간이 얼마나 걸렸는지. 이걸로 돈을 얼마나 줄지 판정해야한다

        if (buildingData == null)
        {
            Debug.Log("빌딩데이터를 못찾았음");
            return;
        }
        Debug.Log("빌딩데이터를 찾았음");
        building.SetData(buildingData);

        if (building.isCompleted)
        {
            //만약 이전부터 완성되었던 건물이라면
            string lastPlayTime = sceneLoader.wholeGameData.lastPlayTime;
            int time = sceneLoader.TimeSubtractionToSeconds(lastPlayTime, System.DateTime.Now);
            time = time / 10;
            coin += time * buildingData.incomeCoin;
            BuildComplete(building);
        }
        else
        {
            int buildTime = sceneLoader.TimeSubtractionToSeconds(building.buildStartTime, System.DateTime.Now);
            if (buildTime > buildingData.buildTime)
            {
                //짓기전에 정산부터 해야한다.
                //만약 지금켰을 때 완성된 건물이라면
                int time = buildTime - buildingData.buildTime;
                time = time / 10;
                coin += time * buildingData.incomeCoin;
                BuildComplete(building);
                //다 지어진 빌딩 로드

            }
            else
            {
                BuildStart(building);
                //안 지어진 빌딩 로드
            }


        }


        //빌딩오브젝트를 만들어주고, 콜라이더를 씌워서 클릭할 수 있게 만들어준다
        //콜라이더의 싸이즈는 빌딩에 맞게 가로 세로를 넣어주낟.
    }

    //지어지고 있던 빌딩들이 건설되었을 떄 부르는 함수.
    void BuildComplete(int index)
    {
        //index는 buildingList에서 완료된 것의 index를 받는다
        Building building = buildingList[index];
        BuildingData data = building.GetData();
        //data를 받는 이유는 가로 세로 길이도 알아야해서이다.
        building.isCompleted = true; //완성됐다고 해주고
        Destroy(building.buildingObject);
        //기존의 건설중 오브젝트를 뿌시고
        //로드된거면 기존 오브젝트가 없다
        coinIncomeSum += data.incomeCoin;

        building.buildingObject = Instantiate(data.prefab, building.positionVector, Quaternion.identity);
        building.buildingObject.tag = objectTag;
        BoxCollider collider =  building.buildingObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(data.width, 1, data.height);
        //나머지는 건설중오브젝트 만드는거랑 똑같다

        mileage += data.mileage *(nowStage +1);
        //마일리지 한다.
        

        Debug.Log("건설 끝났어");
        
    }

    //이미 다 지어진 빌딩을 짓는 함수. 로딩떄 부름
    void BuildComplete(Building building)
    {
        //index는 buildingList에서 완료된 것의 index를 받는다
        BuildingData data = building.GetData();
        //data를 받는 이유는 가로 세로 길이도 알아야해서이다.
        building.isCompleted = true; //완성됐다고 해주고
        coinIncomeSum += data.incomeCoin;

        building.buildingObject = Instantiate(data.prefab, building.positionVector, Quaternion.identity);
        building.buildingObject.tag = objectTag;
        BoxCollider collider = building.buildingObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(data.width, 1, data.height);
        //나머지는 건설중오브젝트 만드는거랑 똑같다
        Debug.Log("건설 끝났어");

    }


    void BuildStage1LandMark()
    {
         //먼저 데이터부터 뽑아야한다
        string name = "zone1Landmark";
        //빌딩이름이 뭔지 제이슨에서 찾아준다. 귀찮ㅇㅁ.
        BuildingData landmarkData = FindBuildingData(name);

        if (landmarkData == null)
        {
            Debug.Log("좃됐따 랜드마크가 없다");
        }
        else
        {
            Debug.Log("개잘댐ㅋㅋ");
            BuildStart(landmarkData);
            //이제 빌드를 때려준다.
        }
        //Building landmark = new Building()
    }

    IEnumerator BuildCoroutine(){
        while(true)
        {

            for (int i = 0; i < incompletedIndexList.Count; i++)
            {
                //Debug.Log(incompletedIndex.Count + " count");
                Building building = buildingList[incompletedIndexList[i]];
                //Debug.Log(building.buildingName + " checking completed");
                //지나간시간이 빌드타임보다 길면 되는거잖아.
                int buildTime = building.GetData().buildTime;
                //int buildTime = 0;  //디버그용으로 전부1초만에 지어짐 ㅋㅋ
                if (buildTime < sceneLoader.TimeSubtractionToSeconds(building.buildStartTime, System.DateTime.Now))
                {
                    BuildComplete(incompletedIndexList[i]);
                    Debug.Log(incompletedIndexList[i]);
                    incompletedIndexList.Remove(incompletedIndexList[i]);

                    //그러면 미완성 리스트에서 빼준당.
                    i--;
                    //하나를 뺴주면은 인덱스가 하나씩 당겨진다. i도 그거에 맞게 당겨줘야한다.
                
                }
            }
            CoinIncome();
            yield return new WaitForSeconds(1f);
            //코인도 여기서 재준다. 빌드코루틴이면 안되는데, 새 코루틴 만들기 귀찬핟..

            //먼저 이렇게 했을 때 문제 있는지 확인해보고, 없으면 그대로 간다.
        }


    }


    //rayScript에서 실행하는 함수. changedOBject는 position이 변경될 오브젝트다.
    public void ChangeBuildingPosition(GameObject changedObject)
    {
        Building building = FindBuilding(changedObject);

        building.positionVector = changedObject.transform.position;

        //포지션을 저장해준다.
    }

    //코루틴에서 매 초에 한번 씩 불리는 코인타이머
    void CoinIncome()
    {
        coinTimer++;
        optionManager.ChangeText(coin, coinIncomeSum, mileage, coinTimer);
        if(coinTimer >= 10)
        {
            coinTimer = 0;
            coin += coinIncomeSum * (nowStage+1);
        }

        //코인을 늘려준다. 스테이지 + 1만큼
    }

    public void SetSceneLoader()
    {
        sceneLoader.SetGameManager(this);
    }

    public StageData GetStageData()
    {
        stageData.mileage = mileage;    //마일리지 다시 저장

        return stageData;
    }

    void BuyBuilding(string name)
    {
        BuildingData data = FindBuildingData(name);

        if (coin < data.cost)
        {
            return;
            //못사면 그냥 리턴
        }

        coin -= data.cost;
        boughtBuildingList.Add(name);

        //사면은 UI에 버튼이 달라지거나 하는 효과를 넣는다.
        //UI나오고 나서 해야함

    }

    //이름을가지고 데이터를 찾아낸다
    BuildingData FindBuildingData(string name)
    {
        BuildingData buildingData = null;
        foreach (BuildingData data in buildingDataList)
        {
            if (data.buildingName == name)
            {
                buildingData = data;
                break;
            }
            //그 이름을 가지는게 있는지 빌딩데이터에서 찾아야한다.
        }
        if(buildingData == null)
        {
            Debug.Log("find building data null");
        }
        return buildingData;
    }

    public Building FindBuilding(GameObject obj)
    {
        Building building = null;   //못 찾았을 경우를 대비하여 null을 넣어준다
        foreach (Building data in buildingList)
        {
            if (data.buildingObject == obj)
            {
                building = data;
                //게임오브젝트를 찾아서 넣어준다
                break;
            }

        }
        if (building == null)
        {
            Debug.Log("find 불가. building이 null이다");
            return null;
        }

        return building;
    }

    public void BuildRadomBuilding()
    {
        int randomNumber = UnityEngine.Random.Range(0, buildingDataList.Count);
        while (buildingDataList[randomNumber].prefab.name.Contains("Cube"))
        {
            randomNumber = UnityEngine.Random.Range(0, buildingDataList.Count);
        }
        BuildStart(buildingDataList[randomNumber]);
    }

    //when buildings collides each other
    //RayScript.cs calls this method
    public Vector3 OnBuildingCollision(Building building, Vector3 rawPos)
    {
        // 1. it returns building to know about width, height
        // 2. it should know about the buildings that are colliding
        // 3. so it can find the best positon between lots of colliding buildings;
        // tlqkf akfdltnlqwl roTlqkf;


        return new Vector3(0, 0, 0);

    }

}

