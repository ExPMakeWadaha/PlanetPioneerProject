using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    //dumy 오브젝트들을 일단 쓴다.
    public GameObject[] buttonObjects;
    public GameObject[] stageLockObject;
    Text[] textArray;
    //버튼의 오브젝트들 dumy.   

    //이 아래로는 not a dumy
    SceneLoader sceneLoader;
    WholeGameData wholeGameData;
    StageData[] stageDataArray;
    bool[] isStageUnlocked;


    LineRenderer renderer;
    public GameObject lineObject;
    public GameObject lineParent;

    void Start()
    {
        sceneLoader = SceneLoader.singleTone;
        sceneLoader.WholeGameDataLoad();
        textArray = new Text[3];
        //스태틱변수는 이렇게 클래스에서 바로 받아올 수 있따.
        isStageUnlocked = new bool[3];
        /*
        for (int i = 0; i < 3; i++)
        {
            textArray[i] = buttonObjects[i].GetComponentInChildren<Text>();
            isStageUnlocked[i] = false;
            //text를 받아온다.
        }*/
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
            stageLockObject[0].SetActive(true);
        }
        if (stageDataArray[1].mileage >= 100000)
        {
            isStageUnlocked[2] = true;
        }
        else
        {
            stageLockObject[1].SetActive(true);
        }

       // LineMaker();
        //라인프리팹 만들기용으로 만들어놓은거. 안쓰는거다
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


    //안쓰는 함수. 라인만들기 때 코드가 필요해서 만듦
    void LineMaker()
    {

        for (int i = -15; i < 16; i++)
        {
            GameObject inst = Instantiate(lineObject, lineParent.transform);
            LineRenderer rend = inst.GetComponent<LineRenderer>();

           

            Vector3[] positions = new Vector3[2];
            positions[0] = new Vector3(i, 0, 15);
            positions[1] = new Vector3(i, 0, -15);
            if(i%5 == 0)
            {
                rend.startWidth = 0.05f;
                rend.endWidth = 0.05f;
            }
            else
            {
                rend.startWidth = 0.02f;
                rend.endWidth = 0.02f;
            }
            rend.startWidth = 0.05f;
            rend.endWidth = 0.05f;
            rend.SetPositions(positions);

            inst = Instantiate(lineObject, lineParent.transform);
            rend = inst.GetComponent<LineRenderer>();
            if (i % 5 == 0)
            {
                rend.startWidth = 0.1f;
                rend.endWidth = 0.1f;
            }
            else
            {
                rend.startWidth = 0.02f;
                rend.endWidth = 0.02f;
            }
            rend.startWidth = 0.05f;
            rend.endWidth = 0.05f;
            positions[0] = new Vector3(15, 0, i);
            positions[1] = new Vector3(-15, 0, i);
            rend.SetPositions(positions);

        }

        //rend.SetPositions
    }
}
