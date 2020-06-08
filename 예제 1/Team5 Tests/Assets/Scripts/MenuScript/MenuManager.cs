using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    //dumy 오브젝트들을 일단 쓴다.
    public GameObject[] buttonObjects;
    Text[] textArray;
    //버튼의 오브젝트들 dumy.

    //이 아래로는 not a dumy
    SceneLoader sceneLoader;
    WholeGameData wholeGameData;
    StageData[] stageDataArray;
    bool[] isStageUnlocked;


    void Start()
    {
        sceneLoader = SceneLoader.singleTone;
        textArray = new Text[3];
        //스태틱변수는 이렇게 클래스에서 바로 받아올 수 있따.
        isStageUnlocked = new bool[3];
        for (int i = 0; i < 3; i++)
        {
            textArray[i] = buttonObjects[i].GetComponentInChildren<Text>();
            isStageUnlocked[i] = false;
            //text를 받아온다.
        }
        isStageUnlocked[0] = true;
        wholeGameData = sceneLoader.wholeGameData;
        stageDataArray = wholeGameData.stageArray;
        //값을 초기화해주는 작업


        //마일리지가 얼마나 있느냐에 따라 해금이 되는 것. 나중에 또 고쳐줘야함.
        if (stageDataArray[0].mileage >= 50000)
        {
            isStageUnlocked[1] = true;
        }
        else
        {
            textArray[1].text = "2 section is LOCKED";
        }
        if (stageDataArray[1].mileage >= 100000)
        {
            isStageUnlocked[2] = true;
        }
        else
        {
            textArray[2].text = "3 section is LOCKED";
        }
    }


    //구역버튼 클릭되면은 씬로딩을 한다. 씬 로딩은 나중에 애니메이션 넣어준다
    public void OnButtonClick(int index)
    {
        //index = 0,1,2
        if (!isStageUnlocked[index])
        {
            return;
        }
        sceneLoader.LoadScene("GamePlayScene", index);
    }
}
