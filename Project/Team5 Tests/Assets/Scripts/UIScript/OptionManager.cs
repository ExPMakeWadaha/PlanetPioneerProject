using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    
    
    public GameObject optionCanvas; //환경설정창이 켜져있는 창이다. 
    //각 버튼에 해당하는 함수들을 짜주어야 한다.
    // 버튼에서도 매개변수를 넣을 수 있으니, 코드가 겹치지 않게 만들어 준다.
    public GameObject optionButton;
    //옵션 버튼이 눌리면 옵션버튼은 꺼주어야한당.
     

    public GameManager gameManager;
    //옵션이 켜졌을 때 그 사실을 gameManage로 넘겨주어야 한다.
    //이렇게 매니저들끼리 연결되었을 떄 스파게티코드가 와장창 나온다. 진짜 조심해야한다. 근데 방법이 없다.
    public cameraMove cameraMover;


    public Text coinText;
    public Text mileageText;
    public Text incomeSumText;

    public Text coinChangeText;
    public Text mileageChangeText;

    RectTransform coinChangeObj;
    RectTransform  mileageChangeObj;

    Vector3 coinOriginalPos;
    Vector3 mileageOriginalPos;

    int stage;
    int[] wholeMileage;
    int nowStar;
    int nowMileage;
    bool coinCoroutine;
    bool mileageCoroutine;
    float coinTime;
    float mileageTime;

    //스테이지마다 UI가 바뀔건데 그 오브젝트
    //0이 1스테이지, 1이 2스테이지, 2가 3스테이지다
    public GameObject[] uiParentPrefab;
    public GameObject[] buildingButtonPrefab;
    GameObject starParent;

    //instantiate 할 때 캔버스의 자식으로 inst해야돠ㅣ서 그렇다
    public GameObject canvas;

    bool isStoreOpened;
    public GameObject storeScrollObject;


    public List<BuildingData> buildingDataList;



    //해상도 바꿔주는 버튼에 해당하는 함수이다. width랑 height는 버튼창에서 각각 넣어준다.
    //매개변수가 버튼에서 하나밖에 지정을 못해서, width만 매개변수로 하고 height는 width값이랑 맞춰서 넣어준다
    public void ChangeResolution(int width)
    {
        int height = 1280;
        if (width == 1080)
        {
            height = 1920;
        }
        else if(width == 1440)
        {
            height = 2560;
        }

        Screen.SetResolution(width, height, true);
        //이건 구글링하니까 나왔다. 해상도 변경할때 유의할 점이 있다
        //해상도는 변하는데 UI크기는 똑같에서 낮은해상도에서 버튼이 안보일 때가 있다.
        //그래서 Canvas인스펙터의 CanvasScaler에서 적정 해상도를 1080*1920으로 지정하여야 한다.
        //그래야 해상도가 변해도 UI들의 크기도 그것에 맞춰서 변할것이다. 
    }

    //옵션버튼 눌렀을 때 시간이 멈추고, 옵션창이 켜져야 한다. 
    //근데 이 함수는 Back to Game버튼에서도 사용할 수 있게 만들 수 있다
    //옵션버튼이 눌렸을 떄, Back to Game버튼이 눌렸을 떄 이 함수를 사용하게 만들자.
    public void OptionToggle()
    {
        gameManager.Optioning();
        //옵션을 지금 건드리고 있다는 걸 알려주어야한다.

        optionCanvas.SetActive(!optionCanvas.activeSelf);
        //active는 GameObject가 active한지 아닌지 나타내는 값이다. active하면 켜져있는 것이다
        //에디터에서 GameObject의 Inspector에서 이름 왼쪽에 체크창이 있다. 이게 activeSelf값이다.
        //체크 되어있으면 사용한다는 뜻이다. 체크 풀면 오브젝트가 사라진다.
        //그래서 옵션버튼이 눌리면 환경설정창을 켜야하니까 이렇게 하는것이당.


        optionButton.SetActive(!optionButton.activeSelf);
        //버튼이 눌리면 버튼은 사라져야한당.

    }

    //SceneLoader를 가져와서 스타트씬으로 옮겨준다.
    //SceneLoader에서 아까 우리가 Static한 singleTone을 만들어줬다.
    //static한 객체는 public으로 선언해서 값을 안넣어줘도 그냥 가져올 수 있다. 그렇게 하자.
    public void BackToStartscene()
    {
        SceneLoader.singleTone.LoadScene("StartScene", 0);
        //싱글톤이 있으면 절라 간단하다. 만약 싱글톤이 없었으면 이렇게 하면 된다.
        /*
        SceneLoader sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        sceneLoader.LoadScene("StartScene");
        */
        //SceneLoader라는 게임오브젝트를 찾고, 그 오브젝트 아래의 SceneLoader 객체를 찾아서 선언해주어야 한다.
        //그냥 싱글톤으로 하면 한줄 줄어서 쉽다는 뜻이다
    }

    //ExitGame눌렸을떄 함수다. 그냥 게임 꺼준다.
    public void ExitGame()
    {
        Application.Quit();
        //간단.
    }

    public void BuildRandomBuilding()
    {
        gameManager.BuildRadomBuilding();
    }



    //게임매니저에서 매 초마다 부르는 함수
    /*
    public void ChangeText(int coin, int incomeSum, int mileage)
    {
        if (wholeMileage == null)
        {
            return;
        }
        //coin first
        coinText.text = coin.ToString();
        incomeSumText.text = incomeSum.ToString();
        /*
        int mileagePercent = 10 * mileage / wholeMileage[stage];

        if (nowStar >= 9 || nowMileage == mileage)
        {
            return;
        }
        nowMileage = mileage;
        if(mileagePercent >= 10)
        {
            mileagePercent = 9;
        }
        if (nowStar < mileagePercent)
        {
            nowStar = mileagePercent;
            for (int i =0;i<nowStar; i++)
            {
                starParent.transform.GetChild(i).gameObject.SetActive(true);
            }
            
        }
    }*/

    public void CoinChange(int nowCoin, int changedCoin)
    {
        coinText.text = nowCoin.ToString();
        StringBuilder builder;
        if (changedCoin>0)
        {
            coinChangeText.color = new Color(1, 1, 1, 1);
            builder = new StringBuilder("+ ");
        }
        else
        {
            coinChangeText.color = new Color(1, 0.5f, 0, 1);
            builder = new StringBuilder("- ");
            changedCoin *= -1;
        }
        builder.Append(changedCoin.ToString());
        coinChangeText.text = builder.ToString();
        coinChangeObj.anchoredPosition = coinOriginalPos;


        if (coinCoroutine)
        {
            coinTime = 0;
        }
        else
        {
            coinCoroutine = true;
            StartCoroutine(CoinChangeCoroutine(changedCoin));
        }
        

        
    }

    public void MileageChange(int mileage,int incomeMileage)
    {

        int mileagePercent = 10 * mileage / wholeMileage[stage];
        mileageText.text = mileagePercent.ToString();

        StringBuilder builder;
        mileageChangeText.color = new Color(1, 1, 0, 1);
        builder = new StringBuilder("+ ");
        builder.Append(incomeMileage.ToString());
        mileageChangeText.text = builder.ToString();
        mileageChangeObj.anchoredPosition = mileageOriginalPos;



        if (mileageCoroutine)
        {
            mileageTime = 0;
        }
        else
        {
            mileageCoroutine = true;
            StartCoroutine(MileageChangeCoroutine(incomeMileage));
        }

        if (nowStar >= 9 || incomeMileage == 0)
        {
            return;
        }
        if (mileagePercent >= 10)
        {
            mileagePercent = 9;
        }
        if (nowStar < mileagePercent)
        {
            nowStar = mileagePercent;
            for (int i = 0; i < nowStar; i++)
            {
                starParent.transform.GetChild(i).gameObject.SetActive(true);
            }

        }
    }

    public void OnGameLoad(int coin, int mileage)
    {
        coinText.text = coin.ToString();
        //don't need to call corouitne
        int mileagePercent = 10 * mileage / wholeMileage[stage];
        mileageText.text = mileagePercent.ToString();

        if (nowStar >= 9)
        {
            return;
        }
        if (mileagePercent >= 10)
        {
            mileagePercent = 9;
        }
        if (nowStar < mileagePercent)
        {
            nowStar = mileagePercent;
            for (int i = 0; i < nowStar; i++)
            {
                starParent.transform.GetChild(i).gameObject.SetActive(true);
            }

        }
    }
   

    //
    public void StoreButtonClick()
    {

        if (isStoreOpened)
        {
            storeScrollObject.SetActive(false);
            cameraMover.Buying(false);
        }
        else
        {
            storeScrollObject.SetActive(true);
            cameraMover.Buying(true);
        }
        isStoreOpened = !isStoreOpened;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        isStoreOpened = false;
        stage = gameManager.nowStage;
        if(stage >= 3)
        {
            return;
        }

        coinChangeObj = coinChangeText.rectTransform;
        mileageChangeObj = mileageChangeText.rectTransform;

        coinCoroutine = false;
        mileageCoroutine = false;
        coinTime = 0;
        mileageTime = 0;
        coinOriginalPos = coinChangeObj.anchoredPosition;
        mileageOriginalPos = mileageChangeObj.anchoredPosition;

        uiParentPrefab[stage].SetActive(true);

        //getChild가 오브젝트 아래에서 찾아내는거다
        storeScrollObject = uiParentPrefab[stage].transform.GetChild(0).gameObject;

        Transform contentObject = storeScrollObject.transform.GetChild(0).GetChild(0).GetChild(0);
        buildingDataList = gameManager.buildingDataList;

        RectTransform contentRect = contentObject.GetComponent<RectTransform>();
        float height = 250 * (buildingDataList.Count -3);
        contentRect.sizeDelta = new Vector2(0, height);

        //코인텍스트가 7번 차일드더라..
        coinText = uiParentPrefab[stage].transform.GetChild(7).GetComponent<Text>();
        starParent = uiParentPrefab[stage].transform.GetChild(8).gameObject;
        mileageText = uiParentPrefab[stage].transform.GetChild(11).GetComponent<Text>();
        wholeMileage = new int[3];
        wholeMileage[0] = 50000;
        wholeMileage[1] = 100000;
        wholeMileage[2] = 1000000;
        nowStar = 0;

        Vector3 scale = new Vector3(10, 10, 10);
        Vector3 pos = new Vector3(0, -60, 0);
        int buttonCount = 0;
        for (int i = stage; i < buildingDataList.Count; i++)
        {
            
            if (i == 0)
            {
                i += 3;
            }
            
            GameObject buttonObject = Instantiate(buildingButtonPrefab[stage], contentObject,false);
            RectTransform rect = buttonObject.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(0, height/2 -250 * (buttonCount+1), 0);
            Button button = buttonObject.GetComponentInChildren<Button>();
            string name = buildingDataList[i].buildingName;
            button.onClick.AddListener(()=>BuyBuilding(name));
            Text costText = buttonObject.GetComponentInChildren<Text>();
            int cost = buildingDataList[i].cost * (stage + 1);
            if (i < 6)
            {
                cost = buildingDataList[i].cost;

            }
            costText.text = cost.ToString();

            GameObject buildingObject = Instantiate(buildingDataList[i].prefab, buttonObject.transform, false);


            buildingObject.transform.localPosition = pos;
            buildingObject.transform.localScale = scale;
            if(i>5 && i < 10)
            {
                buildingObject.transform.localScale = scale * 5;
            }
            buttonCount++;

            if (i < 3)
            {
                i += 2;
            }
            else if (i < 6)
            {
                i = 5;
            }
        }
    }



    IEnumerator CoinChangeCoroutine(int coinIncome)
    {
        coinTime = 0;
        coinChangeObj.gameObject.SetActive(true);
        Vector3 pos = coinOriginalPos;
        
        RectTransform rect = coinChangeObj;
        Text text = coinChangeText;
        Color color = text.color;
        
                
        

        while(coinTime <= 2f)
        {
            color = text.color;
            color.a -= Time.deltaTime * 0.5f;
            text.color = color;
            coinTime += Time.deltaTime;
            pos = rect.anchoredPosition;
            pos.y = pos.y - Time.deltaTime * 40;
            rect.anchoredPosition = pos;
            yield return null;
            
        }
        color.a = 1;
        text.color = color;
        rect.anchoredPosition = coinOriginalPos;

        coinChangeObj.gameObject.SetActive(false);
        
        coinCoroutine = false;

        
    }

    IEnumerator MileageChangeCoroutine(int mileageIncome)
    {
        mileageTime = 0;
        mileageChangeObj.gameObject.SetActive(true);
        Vector3 pos = mileageOriginalPos;
        RectTransform rect = mileageChangeObj;
        Text text = mileageChangeText;
        Color color = text.color;



        while (mileageTime <= 3f)
        {
            mileageTime += Time.deltaTime;
            color = text.color;
            color.a -= Time.deltaTime * 1 / 3;
            text.color = color;
            pos = rect.anchoredPosition;
            pos.y = pos.y - Time.deltaTime * 40f;
            rect.anchoredPosition = pos;
            yield return null;
        }
        color.a = 1;
        text.color = color;
        rect.anchoredPosition = mileageOriginalPos;

        mileageChangeObj.gameObject.SetActive(false);

        mileageCoroutine = false;


    }



    public void BuyBuilding(string name)
    {
        gameManager.BuyBuildingButton(name);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
