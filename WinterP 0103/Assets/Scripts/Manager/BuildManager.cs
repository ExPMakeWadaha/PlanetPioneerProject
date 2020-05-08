using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

/*메인시스템 변수*/
    static int money = 0; //자원
    static int human = 1; //신도
    static int makeHuman = 0;//전도
    static int makeMoney = 0;//노동
    //static int humanUsable = human - makeHuman - makeMoney;//잉여신도
    static int change; //날짜 변화 감지를 위한 변수 (전날)
    int makeHumanCoef = 2; //인구증가량 계수
    static int makeMoneyCoef = 2; //자원증가량 계수

/*건설시스템 변수*/
    static int humanLimitCoef = 50; //인구수용량 계수
    static int home = 1; //집 숫자
    public static int stage = 1; //건설 단계
    static int buildRequirHuman = 30; //(건설단계별) 신도 소모량
    static int buildRequirMoney = 30; //(건설단계별) 돈 소모량

/*게임오브젝트 관련 변수*/
    float x, y, z; //랜덤 위치 생성
    Vector3 pos;
    public GameObject[] houses = new GameObject[9]; //인스턴스 저장
    public GameObject house1; //프리팹 저장
    public GameObject house2;
    public GameObject house3;
    public GameObject house4;
    public GameObject house5;
    private GameObject houseParents; //인스턴스를 모아두기 위한 하이아키상의 parents

/*UI관련 변수*/
    public GameObject buildImage;
    public GameObject buildText;
    Text buildLabel;
    public Sprite[] lv = new Sprite[5];
   // private GameObject stagePanel; //건설단계 팝업창
    //private GameObject errorText;

/*get 함수들*/
    public static int getHuman() { return human; }
    public static int getMoney() { return money; }
    public static int getMakeHuman() { return makeHuman; }
    public static int getMakeMoney() { return makeMoney; }
    public static int getHumanLimit() { return humanLimitCoef; }
    public static int getHome() { return home; }
    public static int getBuild() { return buildRequirHuman; }
/*set 함수들(상훈)*/
    public static void setHuman(int delta) { human += delta; }
    public static void setMoney(int delta) { money += delta; }
    public static void setHome(int delta) { home += delta; }


    void humanLimitValid() { //인구수용량 검사
        if ( human > humanLimitCoef*home){
            human = humanLimitCoef * home;
        }
    }

    public static void addMakeHuman(int num){ //전도 투입 함수(단 잔여신도만큼..)
        if (num <= human - makeHuman - makeMoney)
        {
            makeHuman += num;
        }
        else ErrorManager.inst.showError("일을 시킬 사람이 부족해요.");
           
    }
       
    public static void addMakeMoney(int num){ //노동 투입 함수
        if (num <= human - makeHuman - makeMoney)
        {
            makeMoney +=num;
        }
        else ErrorManager.inst.showError("일을 시킬 사람이 부족해요."); 
    }

    public static void subMakeHuman(int num)
    { //전도 투입 함수(단 잔여신도만큼..)
        if (num <=makeHuman)
        {
            makeHuman -= num;
        }
        else ErrorManager.inst.showError("전도차출이 안돼요");

    }

    public static void subMakeMoney(int num)
    { //노동 투입 함수
        if (num <=makeMoney)
        {
            makeMoney -= num;
        }
        else ErrorManager.inst.showError("신도차출이 안돼요.");
    }

    /*건설시스템 관련 함수*/
    public static void buildHumanAction(int r) //r means buildRequirHuman
    {
        int humanUsable = human - makeMoney - makeHuman;

        if (humanUsable - r >= 0) human = human - r;
        else
        {
            if (makeMoney - (r - humanUsable) >= 0)
            {
                human = human - r;
                makeMoney = makeMoney - (r - humanUsable);
            }
            else
            {
                human = human - r;
                makeHuman = makeHuman - (r - humanUsable - makeMoney);
                makeMoney = 0;
            }
        }
    }
    void buildswitch()
    {
            int tmp = home - 1;
            switch (stage)
            {
                case 1: houses[tmp] = Instantiate(house1, pos, transform.rotation) as GameObject;
                    houses[tmp].transform.parent = houseParents.transform;
                    break;

                case 2: houses[tmp] = Instantiate(house2, pos, transform.rotation) as GameObject;
                    houses[tmp].transform.parent = houseParents.transform;
                    break;

                case 3: houses[tmp] = Instantiate(house3, pos, transform.rotation) as GameObject;
                    houses[tmp].transform.parent = houseParents.transform;
                    break;

                case 4: houses[tmp] = Instantiate(house4, pos, transform.rotation) as GameObject;
                    houses[tmp].transform.parent = houseParents.transform;
                    break;

                case 5: houses[tmp] = Instantiate(house5, pos, transform.rotation) as GameObject;
                    houses[tmp].transform.parent = houseParents.transform;
                    break;
            }
            buildHumanAction(buildRequirHuman);
            money = money - buildRequirMoney;
        

        //여기서 인구수용량은 증가하지 않음. 인구수용량은 home의 증가를 통해 증가하기 때문. 
    }

   public void buildHouse() //집 건설 관리 함수
    {
        if (human >= buildRequirHuman && money >= buildRequirMoney)
        {
            if (home < 9) //단계 넘어가기 전
            {
                home++;
                Debug.Log("home: " + home);
                randomPos();
                buildswitch();
                if (home == 9)
                {
                    stage++; //미리 단계를 올려 준다
                    Debug.Log("stage: " + stage);
                    if (stage != 6)
                    {
                        buildImage.GetComponent<Image>().sprite = lv[stage - 1];
                        int num = 3 * (int)Mathf.Pow(10, stage);
                        buildLabel.text = num.ToString() + "            " + num.ToString();
                        buildRequirHuman = 3 * (int)Mathf.Pow(10, stage);
                        buildRequirMoney = 3 * (int)Mathf.Pow(10, stage);
                    }
                }
            }
            else if (home == 9) //다음 단계로 넘어감
            {
                if (human >= 3 * (int)Mathf.Pow(10, stage) && money >= 3 * (int)Mathf.Pow(10, stage))
                {
                    //stage++;
                    //Debug.Log("stage: " + stage);
                    if (stage > 0 && stage < 6)
                    { //stage값이 유효한지 검사
                      /*  buildRequirHuman = 3 * (int)Mathf.Pow(10, stage);
                        buildRequirMoney = 3 * (int)Mathf.Pow(10, stage);
                        humanLimitCoef = 5 * (int)Mathf.Pow(10, stage);*/
                        humanLimitCoef = 5 * (int)Mathf.Pow(10, stage);
                        for (int i = 0; i < 9; i++)
                        {
                            Destroy(houses[i], 0.0F);
                        }
                        home = 1;
                        buildswitch();
                    }
                    else //마지막단계. 필요?
                    {
                        home++;
                        buildHouse();
                    }
                }
            }
            else if (home > 9) //5단계 이후 인구수용량만 증가
            {
                buildHumanAction(buildRequirHuman);
            }
        }
        /* 5번째 집이 지어졌을 때 다음단계 안내 메세지가 뜨고, 자동적으로 다음단계로 넘어간다.???굳이???(stage++) 그러나 집은 없어지지 않음.
           home이 5이면서 buildHouse가 눌렸을 때 이전단계의 집이 destroy, 스테이지 관련 변수들이 바뀌고 home은 1이 되고 2단계 집이 지어짐 */
    }

   public void randomPos()
   {
       x = Random.Range(-1.11f, 1.12f);
       y = Random.Range(-0.8f, 0.83f);
       z = 0;
       pos = new Vector3(x, y, z);
   }

	// Use this for initialization
	void Start () {
        change = TimeManager.getDay(); //전날에 해당
        randomPos();
        houseParents = GameObject.Find("HouseParents");
        //buildswitch();
        
        houses[0] = Instantiate(house1, pos, transform.rotation) as GameObject;
        houses[0].transform.parent = houseParents.transform;
        
        buildLabel = buildText.GetComponent<Text>();
        buildImage.GetComponent<Image>().sprite = lv[0];

        int num = 3 * (int)Mathf.Pow(10, stage);
        buildLabel.text = num.ToString() + "              " + num.ToString();
        //stagePanel = GameObject.Find("PopupParents");
        //stagePanel.SetActive(false);
	}
	
    bool errorValid = true; //true면 에러가 아직 안 뜸, false면 에러가 이미 떴음

	// Update is called once per frame
	void Update () {
        if (TimeManager.getDay() != change){ //(일+1)이면
            //신도와 자원 증가
            if (human < humanLimitCoef * home)
            {
                human = human + (makeHumanCoef * makeHuman);
                errorValid = true;
            }
            else
            {
               if (errorValid==true){
                   ErrorManager.inst.showError("집이 모자라요~집을 지어주세요.");
                   errorValid = false;
               } 
            }
            money = money + (makeMoneyCoef * makeMoney);
            change = TimeManager.getDay();

            humanLimitValid();
            Debug.Log("Human: " + human);
            Debug.Log("Money: " + money);
            Debug.Log("잔여신도" + (human - makeHuman - makeMoney));
            //이벤트 발동
            int DailyEvent = EventManager.Inst.EventProbGenerator();
            if (DailyEvent != -1)
                EventManager.Inst.EventGenerator(DailyEvent);
            else
                Debug.Log("이벤트없음");

        }
	}

    /*리셋할 때 요소들 모두 초기화*/
    public static void GameReset()
    {
        /*메인시스템 변수*/
        money = 0; //자원
        human = 1; //신도
        makeHuman = 0;//전도
        makeMoney = 0;//노동
        humanLimitCoef = 50; //인구수용량 계수
        home = 1; //집 숫자
        stage = 1; //건설 단계
        buildRequirHuman = 30; //(건설단계별) 신도 소모량
        buildRequirMoney = 30; //(건설단계별) 돈 소모량
    }
}
