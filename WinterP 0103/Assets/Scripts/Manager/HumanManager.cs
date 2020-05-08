using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour {
    public GameObject[] humans = new GameObject[15];
    int num;

	// Use this for initialization
	void Start () {
        setHumans(0);
	}
	
	// Update is called once per frame
	void Update () {
        num = BuildManager.getHuman();
        if(num<1) setHumans(0);//1~150 바깥이면 없음
        else if (num >= 151) setHumans(15);//1~10
        else if(num>=1 && num<=10) setHumans(1);//1~10
        else if(num>=11 && num<=20) setHumans(2);//11~20
        else if(num>=21 && num<=30) setHumans(3);//1~10
        else if(num>=31 && num<=40) setHumans(4);//1~10
        else if(num>=41 && num<=50) setHumans(5);//1~10
        else if(num>=51 && num<=60) setHumans(6);//1~10
        else if(num>=61 && num<=70) setHumans(7);//1~10
        else if(num>=71 && num<=80) setHumans(8);//1~10
        else if(num>=81 && num<=90) setHumans(9);//1~10
        else if(num>=91 && num<=100) setHumans(10);//1~10
        else if(num>=111 && num<=120) setHumans(11);//1~10
        else if(num>=121 && num<=130) setHumans(12);//1~10
        else if(num>=131 && num<=140) setHumans(13);//1~10
        else if(num>=141 && num<=150) setHumans(14);//1~10
      
	}
    void setHumans(int n) //n개까지 SetActive true
    { 
        if (n > 15 || n < 0) return;
        for (int i = 0; i < n; i++)
        { //n이 10이면 10개까지.0~9
            humans[i].SetActive(true);
        }
        for (int j = n; j < 15; j++)
        { //10~14까지
            humans[j].SetActive(false);
        }
    }
}
