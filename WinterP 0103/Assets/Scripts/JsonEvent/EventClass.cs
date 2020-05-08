using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//정상훈 180110 15:00

[System.Serializable]
public class EventClass {
    //순번 제복 발생확률 등 이벤트 자체의 발생 요소
    public int index;
    public string title;
    public int level;
    public int rate;
    public int overlap;
    public string icon;
    public string icon_result1;
    public string icon_result2;
    //선택지 텍스트
    public string text;
    public string choice1;
    public string choice2;
    public string description1;
    public string description2;
    public string resultDescription1;
    public string resultDescription2;



    //이벤트 발생 조건
    public int minHuman;
    public int maxHuman;
    public int minHome;
    public int maxHome;
    public int minMoney;
    public int maxMoney;
    public int minEvil;
    public int maxEvil;
    public int minLoyal;
    public int maxLoyal;
    public int minPower;
    public int maxPower;
    public int minDate;
    public int maxDate;





       //선택지1에서의 자원 변동
    public int human1;
    public int home1;
    public int money1;
    public int evil1;
    public int loyal1;

    //선택지2에서의 자원 변동
    public int human2;
    public int home2;
    public int money2;
    public int evil2;
    public int loyal2;

}
