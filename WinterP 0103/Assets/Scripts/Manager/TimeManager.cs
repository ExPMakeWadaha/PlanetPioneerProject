using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour{


    static float TurnTime; //몇초가 흘렀는지 알려줌.
    static int Day; //몇일인지
    static int Month; //몇달인지.
    static int Year; //몇년인지.
    static int AbsoluteDay=0;

    public static int getAbsoluteDay() { return AbsoluteDay; }
    public static float getTurnTime() { return TurnTime;  }
    public static int getDay() { return Day; }
    public static int getMonth() { return Month; }
    public static int getYear() { return Year; }

    float eventProb; //이벤트 발생확률, 랜덤 변수.
    public static bool EventTimePause = true; //TimeManager에서 주로 씀. 이벤트 창이 떴을 때 시간을 멈추기 위한 변수
    public Text TimeLog; // 얼마나 지났는지 알려줌
    public float DayLength; // 몇 deltatime이 1일인지
    public int MonthLength = 30; // 1달이 몇일.

    public void Update () { //타임매니저.

        if(EventTimePause == true) //이벤트 창을 보고있으면 시간이 안감
        {
            TurnTime += Time.deltaTime; //시간이 지날때마다 TurnTime값 증가.

            if (TurnTime >= DayLength) //시간이 지나고 있을 때 만 이벤트 트리거.
            {
                DayUpdate(); //1일 증가.
                 // HumanManagerDumy.dayUpdate();//static으로하면 에러나서 화남ㅡㅡ 180112.
                Debug.Log("Day"+Day); //디버그
                Debug.Log("Month" + Month);
                Debug.Log("Year" + Year);
                TurnTime = 0; //TurnTime초기화
            }
        }
	}

    public static void GameReset()
    {
        Day = 0;
        Month = 0;
        Year = 0;
    }
    
    void DayUpdate()
    {
        Day++;
        AbsoluteDay++;
        if (Day > 30)
        {
            Month++;
            if (Month > 12)
            {
                Year++;
                Month = 1;
            }
            Day = 1;
        }
    }




}
