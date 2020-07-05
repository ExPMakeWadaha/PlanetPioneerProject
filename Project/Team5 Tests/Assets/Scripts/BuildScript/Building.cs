using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

[System.Serializable]
public class Building        //빌딩 하나하나가 가지는 클래스.
{
    public string buildingName;     //건물이름에 맞게 오브젝트를 가져와야한다
    BuildingData buildingData;      //그냥 빌딩 자체가 데이터를 가져버리면 되잖아.
                                    //stagedata에서도 이대로 저장되면 json이 너무 길어지니 안된다.
    GameObject buildingUI;
    Button sellButton;
    Text progressText;
    RectTransform progressRect;
                                    //private으로 해놓으면 제이슨에서 저장을 안한다. 그래서 json파일이 짧아져서 좋다.
    public int index;               //건물이 그 스테이지에서 몇 번째로 생긴 건물인지
    public Vector3 positionVector;             //기하와 벡터에서의 그 벡터 맞다. 공간좌표 맞다.     
                                               //오브젝트의 위치(x,y,z)를 Vector3 클래스를 사용하여 나타낸다.
    //200530 started sanghun

    public bool isCompleted;              //완성되었는지, 완성되면 true
    public string buildStartTime;

    public GameObject buildingObject;



    /*
    building에서 해주어야할 것들
    1. 건물을 구매하여 생성이 되면 그리드로 움직일 수 있게 넘기기
    2. 건물이 자동생성이면 그냥 그자리에 있기
    3. 만약 건물이 건설중이라면 건설중 오브젝트 띄우기
    4. 건물이 완료되면 오브젝트 변경하기
    */


    //빌딩이 처음에 만들어질 때 어떻게 될 것인가.
    public Building(BuildingData data, int indexParameter)
    {
        buildingData = data;
        index = indexParameter;
        buildStartTime = System.DateTime.Now.ToString();
        buildingName = data.buildingName;
        isCompleted = false;
        //그냥 생성자다. 찬찬히 읽어보세요
        //데이터를 집어넣어준다는것만 중요합니다.
    }


    public void SetProgressUI(GameObject obj)
    {

        buildingUI = obj;
        progressText = obj.GetComponentInChildren<Text>();
        progressRect = obj.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
    }

    public RectTransform GetRect()
    {
        return progressRect;
    }

    public void SetSellUI(GameObject obj)
    {
        buildingUI = obj;
        sellButton = buildingUI.GetComponentInChildren<Button>();
        
    }
    public Button GetSellButton()
    {
        return sellButton;
    }


    public GameObject GetUI()
    {
        return buildingUI;
    }

    public Text GetText()
    {
        return progressText;
    }

    public bool IsBuildingNow()
    {
        return true;
    }

    public GameObject LoadedObject()
    {
        return null;
    }

    public BuildingData GetData()
    {
        return buildingData;
    }

    public void SetData(BuildingData data)
    {
        buildingData = data;
    }

}