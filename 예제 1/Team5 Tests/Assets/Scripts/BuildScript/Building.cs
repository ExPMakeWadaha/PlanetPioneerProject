using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building        //빌딩 하나하나가 가지는 클래스.
{
    public int type;            //건물의 종류. 지금은 의미없는 변수
    public int index;           //건물이 몇 번째로 생긴 건물인지
    public int income;          //건물에서 나오는 돈은 얼마인지
    public GameObject buildingObject;   //Building이 현재 가지고 있는 GameObject는 무엇인지
                                        //object를 만드는 이유는 이 클래스의 함수에서
                                        //오브젝트의 애니메이션이나 소멸을 관리해주어야 하기 때문

    Vector3 positionVector;             //기하와 벡터에서의 그 벡터 맞다. 공간좌표 맞다.     
                                        //오브젝트의 위치(x,y,z)를 Vector3 클래스를 사용하여 나타낸다.

    public Building(int typePara, int indexPara, int incomePara, GameObject objectPara)
    {
        //생성자에서 받은 변수를 객체에다가 넣어줍니다.
        type = typePara;
        index = indexPara;
        income = incomePara;
        buildingObject = objectPara;
        positionVector = new Vector3((indexPara / 5) * 2,0, (indexPara % 5) * 2);
        //빌딩 오브젝트가 어디 위치할지 positionVector로 정해준다
        //물론 Vector3도 클래스이기 때문에 객체를 new로 할당해주어야 이용할 수 있따. 
        objectPara.transform.position = positionVector;
        //오브젝트의 위치를 object.trasnform.position = (Vector3객체 무언가); 의 형식으로 이동시킬 수 있다
        Debug.Log("전나잘됨");
    }

    public int incomeCollect()
    {
        return income;
        //update에서 income을 불러올 때 사용하는 함수.
    }
    
    

}