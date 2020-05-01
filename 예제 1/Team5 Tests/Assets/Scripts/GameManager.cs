using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public int nowMoney;        //현재 가지고 있는 돈
public GameObject cube;     //큐브
public GameObject sphere;   //구
    GameObject newCube;
    GameObject newSphere;
int nowBuildingIndex;       //현재 빌딩의 개수
float timer;                //타이머
    List<Building> buildingList;    //빌딩들을 동적할당 할 수 있는 리스트(C++ 에서의 array *)
                                    //C#에서의 동적할당은 Array가 아닌 List로 한다.
                                    //포인터 개념을 사용하지 않기 때문에 C++에서의 동적할당 방법은 사용하지 않는다.
                                    //대신 이미 있는 라이브러리인 List를 써준다.List가 궁금하면 API를 찾아보길 바란다.
    void Start()
{
        //  ...............여기다가 작성하세용...........
        nowMoney = 0;
        timer = 0;
        nowBuildingIndex = 0;
        newCube = Instantiate(cube);
        buildingList = new List<Building>();    //List또한 객체이기 때문에 빈 List객체를 new로 할당하여야 한다
        Building b1 = new Building(1, nowBuildingIndex++, 1, newCube);   //지역변수 b1을 통해 새 객체를 만들어주고, 이 객체를 List에 Add하여야 한다.
        buildingList.Add(b1);                   //b1을 Add해준당
        // 생성자가 생각이안나서 썼따 (int typePara, int indexPara, int incomePara, GameObject objectPara)
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.F1))
    {
            // ...............여기다가 작성하세용........…
            //a가 눌릴때 작동되는 함수
            newCube = Instantiate(cube);
            Building b2 = new Building(1, nowBuildingIndex++, 1, newCube);
            buildingList.Add(b2);
            Debug.Log("f1");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            // ...............여기다가 작성하세용........…
            //b가 눌릴때 작동되는 함수
            Debug.Log("f2");
            newSphere = Instantiate(sphere);
            Building b3 = new Building(1, nowBuildingIndex++, 1, newSphere);
            buildingList.Add(b3);
        }
        if (timer >= 2)
        {
            timer = 0;
            for(int i =0;i<buildingList.Count; i++)
            {
                nowMoney += buildingList[i].incomeCollect();
            }
            Debug.Log("Now Money is " + nowMoney);
        }
        timer += Time.deltaTime;

    //...............여기다가 작성하세용...........

}


}