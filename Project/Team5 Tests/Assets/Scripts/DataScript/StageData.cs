using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class StageData
{

    public int stageIndex;                     //몇번쨰 구역인지(0,1,2)
    public List<Building> buildingList;        //현재 빌딩의 개수
    public int mileage;                        //명성치
    public float progressRate;                 //진척도
    public bool[] achievementArray;            //업적 달성한게 뭔지
    public List<int> incompletedIndex;         //미완성된 건물의 인덱스
    public List<string> boughtBuilidng;        //구매해놓고 안산 건물.
    public string lastPlayTime;                        //마지막 플탐을 알아야 건물을 세운다


    //게임 처음 시작할 때의 생성자입니다.
    /*
    public StageData()
    {
        //nothing yet
        stageIndex = 0;
        buildingList = new List<Building>();
        mileage= 0;
        progressRate = 0;
        achievementArray = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            achievementArray[i] = false;
        }
    }*/

    //게임 처음 시작할 때의 생성자입니다.
    //로딩할 때에는 생성자를 불러오지 않습니다. 새로 만들지 않고 있는 데이터를 가져오기 때문입니다.
    public StageData(int index)
    {
        //nothing yet
        stageIndex = index;
        buildingList = new List<Building>();
        incompletedIndex = new List<int>();
        mileage = 0;
        progressRate = 0;
        achievementArray = new bool[10];
        boughtBuilidng = new List<string>();
        lastPlayTime = System.DateTime.Now.ToString();
        for (int i = 0; i < 10; i++)
        {
            achievementArray[i] = false;
        }
    }

}
