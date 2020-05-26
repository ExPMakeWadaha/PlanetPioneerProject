using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
//json을 쓸 곳에는 다 serialzable이 들어가야해요
public class WholeGameData
{
    
    public StageData[] stageArray;                     //받아온 스테이지데이터들 3개.
    public string lastPlayTime;                        //마지막 플탐을 알아야 건물을 세운다
    public string id;                                  //그냥 만들어본 아이디
    public List<Building>[] incompletedBuildingList;   //각스테이지에 존재하는 미완성 건물들


    //TEST생성자입니다.
    public WholeGameData()
    {
        stageArray = new StageData[3];
        
        incompletedBuildingList = new List<Building>[3];
        for(int i = 0; i < 3; i++)
        {
            incompletedBuildingList[i] = new List<Building>();
            stageArray[i] = new StageData(i);
        }
        lastPlayTime = System.DateTime.Now.ToString();
        id = "sanghun";
    }

    //TEST생성자입니다.
    public WholeGameData(StageData[] arr)
    {
        stageArray = arr;
        incompletedBuildingList = new List<Building>[3];
        for (int i = 0; i < 3; i++)
        {
            incompletedBuildingList[i] = new List<Building>();
        }
        lastPlayTime = System.DateTime.Now.ToString();
        id = "sanghun";
    }
    
    
    //TEST생성자입니다.
    public void ChangeStageData(StageData data, int index)
    {
        if (index > 2)
        {
            return;
        }
        else if(stageArray.Length < index)
        {
            return;
        }

        stageArray[index] = data;

    }
}
