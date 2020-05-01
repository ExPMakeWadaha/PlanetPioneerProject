using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building           //빌딩 하나하나가 가지는 클래스.
{
    public int type;            //건물의 종류. 지금은 의미없는 변수
    public int index;           //건물이 몇 번째로 생긴 건물인지
    public int income;          //건물에서 나오는 돈은 얼마인지
    public GameObject buildingObject;   //Building이 현재 가지고 있는 GameObject는 무엇인지
                                        //object를 만드는 이유는 이 클래스의 함수에서
                                        //오브젝트의 애니메이션이나 소멸을 관리해주어야 하기 때문
    Vector3 positionVector;                                        

    public Building(int typePara, int indexPara, int incomePara, GameObject objectPara)
    {
        type = typePara;
        index = indexPara;
        income = incomePara;
        buildingObject = objectPara;
        positionVector = new Vector3((indexPara / 5) * 2,0, (indexPara % 5) * 2);
        objectPara.transform.position = positionVector;
        //생성자에서 받은 변수를 객체에다가 넣어줍니다.
    }

    public int incomeCollect()
    {
        return income;
        //update에서 income을 불러올 때 사용하는 함수.
    }

}