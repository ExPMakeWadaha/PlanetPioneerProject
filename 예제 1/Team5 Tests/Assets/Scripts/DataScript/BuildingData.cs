using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BuildingData
{

    //{"buildingName":"satelite2","type":0,"incomeTime":10800,"incomeCoin":100,"buildTime":5,"cost":1000,"width":5,"height":5},
    public string buildingName;     //빌딩의 이름
    public int type;                //랜드마크인지 아닌지,
    public int incomeTime;          //돈이 들어오는 주기(초)
    public int incomeCoin;          //incomeCoin
    public int buildTime;           //건설에 걸리는 시간(초)
    public int cost;                //드는 비용
    public int width;               //garo             
    public int height;              //sero

}
