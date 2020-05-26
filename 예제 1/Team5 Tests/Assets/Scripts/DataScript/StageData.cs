using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class StageData
{
    public int stageIndex;                     //몇번쨰 구역인지(0,1,2)
    public List<Building> buildingList;        //현재 빌딩의 개수
    public int coin;                           //현재 돈
    public int reputation;                     //명성치
    public float progressRate;                 //진척도
    public bool[] achievementArray;            //업적 달성한게 뭔지


    //더미생성자입니다.
    public StageData()
    {
        //nothing yet
        stageIndex = 0;
        buildingList = new List<Building>();
        coin = 2000;
        reputation = 1000;
        progressRate = 90.1f;
        achievementArray = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                achievementArray[i] = true;
            }
            else
            {
                achievementArray[i] = false;
            }
        }
    }

    //더미생성자입니다.
    public StageData(int index)
    {
        //nothing yet
        stageIndex = 0;
        buildingList = new List<Building>();
        coin = 2000*index;
        reputation = 1000*index;
        progressRate = 90.1f*index;
        achievementArray = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                achievementArray[i] = true;
            }
            else
            {
                achievementArray[i] = false;
            }
        }
    }

}
