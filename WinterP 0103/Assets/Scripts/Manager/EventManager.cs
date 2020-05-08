using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EventManager : TimeManager {
    
    [HideInInspector]
    public static EventManager Inst; //전체에 static 안 붙이려고 해놓은 조치


    /*이벤트UI관련 변수*/
    GameObject BackGround; //회색 배경
    GameObject EventPanel; //이벤트 띄워주는 패널
    GameObject[] EventButton = new GameObject[3]; //yes, no 버튼. 버튼 0,1 은 2개초이스일때, 버튼3은 초이스없을때
    GameObject ResultButton;
    GameObject EventImage; //이벤트 이미지 나오는거
    GameObject EventText; // 이벤트 설명나오는거
    GameObject[] choiceBalloon = new GameObject[3]; //이벤트 선택 말풍선
    GameObject[] choiceBalloonText = new GameObject[3]; //이벤트 선택 설명
    private bool[] choiceTextBool = new bool[3];



    /*이벤트 발생함수 관련 변수*/
    public struct EventsAvailableClass //현재 조건을 충족한 이벤트의 개수들
    {
        public int StackRate;
        public int EventsRate;
        public int EventsIndex;
    }

    bool[] EventOverlapIndex = new bool[92]; //1번만 일어나는 이벤트 배열. 
    int DailyEventIndex; //현재 일어난 이벤트 인덱스. 여러 함수에 걸쳐 써야돼서 어쩔수없이 전역변수로 만듦
    public  int EventAbsoluteProb; //이벤트 절대발생 확률. 단위는 %
    public EventClass[] AllEventList; // 모든 이벤트의 배열. 이벤트의 요소를 가져올 때 쓰임. 0번이벤트는 AllEventList[0], n번째이벤트는 AllEventList[n
    public int AvailableEventLength = 0; //활성화된 이벤트의 개수


    private void Awake()
    {
        Screen.SetResolution(720, 1080, true); //안드로이드에서 화면비율 고정.

        /*static대신 쓰는거*/
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        /*변수하고 오브젝트하고 이어주는 선언문*/
        BackGround = GameObject.Find("BackGround");
        EventPanel = GameObject.Find("EventPanel");
        EventButton[0] = GameObject.Find("EventButton1");
        EventButton[1] = GameObject.Find("EventButton2");
        EventButton[2] = GameObject.Find("EventButton3");
        ResultButton = GameObject.Find("ResultButton");
        EventImage = GameObject.Find("EventImage");
        EventText = GameObject.Find("EventText");
        choiceBalloon[0] = GameObject.Find("choice1Balloon");
        choiceBalloon[1] = GameObject.Find("choice2Balloon");
        choiceBalloon[2] = GameObject.Find("choice3Balloon");

        choiceBalloonText[0] = GameObject.Find("choice1Text");
        choiceBalloonText[1] = GameObject.Find("choice2Text");
        choiceBalloonText[2] = GameObject.Find("choice3Text");

        /*UI들을 숨겨주는 역할*/
        ResultButton.SetActive(false);
        EventPanel.SetActive(false);
        for(int i=0; i < 3; i++)
        {
            EventButton[i].SetActive(false);
            choiceBalloon[i].SetActive(false);
        }
        EventImage.SetActive(false);

        /*이벤트 관련 함수 초기화*/
        for(int i=0; i < 30; i++)
        {
            EventOverlapIndex[i] = true;
        }
        EventPanel.SetActive(false);
        AllEventList = JsonManager.getEventListJson<EventClass>("Events.json"); //이벤트 리스트로 제이슨파일을 가져옴.
    }


    public int EventProbGenerator()
    {
        /*활성화된 이벤트를 받고, 확률에 맞게 이벤트인덱스 리턴*/
        int randomAbsolute = Random.Range(0, 100); //절대 이벤트 발생랜덤변수
        if (randomAbsolute <= EventAbsoluteProb)
        {
            EventAbsoluteProb = 10; //절대이벤트 발생 시, 스택없애고 절대확률 초기화
                                    /*빌드매니저에서 현재 변수값들 가져오기*/
            int human = BuildManager.getHuman();
            int money = BuildManager.getMoney();
            int home = BuildManager.getHome();
            int Evil = -1;
            int Loyal = -1;
            int Power = -1;
            int Date = getAbsoluteDay();

            /*발동조건 체크단계*/
            //EventChecker을 그대로 가져왔음. EventsAvailableArray
            int[] dumyArray = new int[30]; //EventsAvailableArray에 미리 넣으면 참 좋을텐데.

            for (int i = 0; i < AllEventList.Length; i++)
            {
                if (human >= AllEventList[i].minHuman && (human < AllEventList[i].maxHuman || AllEventList[i].maxHuman == -1))
                {
                    if (money >= AllEventList[i].minMoney && (money < AllEventList[i].maxMoney || AllEventList[i].maxMoney == -1))
                    {
                        if (Evil >= AllEventList[i].minEvil && Evil < AllEventList[i].maxEvil || AllEventList[i].maxEvil == -1)
                        {
                            if (Loyal >= AllEventList[i].minLoyal && Loyal < AllEventList[i].maxLoyal || AllEventList[i].maxLoyal == -1)
                            {
                                if (Power >= AllEventList[i].minPower && Power < AllEventList[i].maxPower || AllEventList[i].maxPower == -1)
                                {
                                    if (Date >= AllEventList[i].minDate && (Date < AllEventList[i].maxDate || AllEventList[i].maxDate == -1))
                                    {
                                        if (home >= AllEventList[i].minHome && (home < AllEventList[i].maxHome || AllEventList[i].maxHome == -1))
                                        {
                                            if (EventOverlapIndex[i] == true)
                                            {
                                                dumyArray[AvailableEventLength] = i;
                                                AvailableEventLength++;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (AvailableEventLength == 0)
                return -1;
            EventsAvailableClass[] EventsAvailableArray = new EventsAvailableClass[AvailableEventLength];

            for (int i = 0; i < AvailableEventLength; i++)
            {
                EventsAvailableArray[i].EventsIndex = dumyArray[i];
                EventsAvailableArray[i].EventsRate = AllEventList[dumyArray[i]].rate;
            }


            int wholeRate = 0; //이벤트들의 각 발생확률 더한것(분모)
            int RandomNumber = 0; //랜덤값 넣어줄 변수

            EventsAvailableArray[0].StackRate = EventsAvailableArray[0].EventsRate; //stack쌓는거 첫단계, 초항
            wholeRate += EventsAvailableArray[0].EventsRate;//초항

            for (int i = 1; i < AvailableEventLength; i++)
            {
                wholeRate += EventsAvailableArray[i].EventsRate;
                EventsAvailableArray[i].StackRate = EventsAvailableArray[i - 1].StackRate + EventsAvailableArray[i].EventsRate;
                //이전 배열의 stack에 자신의 events를 더해 자신의 stack을 만든다.i에서 i-1의 StackRate를 빼면 i의 EventsRate가 나온다.
            }

            RandomNumber = Random.Range(0, wholeRate);

            if (RandomNumber <= EventsAvailableArray[0].StackRate)
            {
                if (AllEventList[EventsAvailableArray[0].EventsIndex].overlap == 0)
                    EventOverlapIndex[EventsAvailableArray[0].EventsIndex] = false;
                AvailableEventLength = 0;
                return EventsAvailableArray[0].EventsIndex; //초항
            }
            else
            {
                for (int i = 1; i < AvailableEventLength; i++)
                {
                    if (RandomNumber >= EventsAvailableArray[i - 1].StackRate && RandomNumber <= EventsAvailableArray[i].StackRate)
                    {
                        if (AllEventList[EventsAvailableArray[i].EventsIndex].overlap == 0)
                            EventOverlapIndex[EventsAvailableArray[i].EventsIndex] = false;
                        AvailableEventLength = 0;
                        return EventsAvailableArray[i].EventsIndex;
                    }
                }
            }
            AvailableEventLength = 0;
            return -1; //아 다햇다.
        }
        else
        {
            AvailableEventLength = 0;
            EventAbsoluteProb += 10;
            return -1;
        }
    }

    public void EventGenerator(int EventIndex)
    {
        Image myImage;
        Image[] CheckImage = new Image[3];
        Text myText;
        Text[] choiceText  = new Text[3];
        Debug.Log("이벤트는" + EventIndex);
        string newPath;

        SoundManager.instance.PlaySound("eventSound");
        BackGround.SetActive(true);
        EventTimePause = false;
        EventPanel.SetActive(true);
        DailyEventIndex = EventIndex;
        newPath = AllEventList[DailyEventIndex].icon;

        for(int i = 0; i < 3; i++)
        {
            choiceTextBool[i] = false;
            choiceBalloon[i].SetActive(false);
            choiceBalloonText[i].SetActive(false);
            choiceText[i] = choiceBalloonText[i].GetComponent<Text>();
            CheckImage[i] = EventButton[i].GetComponent<Image>();
        }
        
        myText = EventText.GetComponent<Text>();
        myImage = EventImage.GetComponent<Image>();
        newPath = newPath.Replace(".png", "");
        myImage.sprite = Resources.Load<Sprite>(newPath);
        myText.text = AllEventList[DailyEventIndex].text;

        EventImage.SetActive(true);

        if (AllEventList[EventIndex].choice2 == "-1")
        {
            CheckImage[2].sprite = Resources.Load<Sprite>("Buttons/event_singlebutton");
           EventButton[2].SetActive(true);
           choiceText[2].text = AllEventList[DailyEventIndex].choice1;
        }
        else
        {
            CheckImage[0].sprite = Resources.Load<Sprite>("Buttons/event_prototype_yes");
            CheckImage[1].sprite = Resources.Load<Sprite>("Buttons/event_prototype_no");
            EventButton[0].SetActive(true);
           EventButton[1].SetActive(true);
            choiceText[0].text = AllEventList[DailyEventIndex].choice1;
            choiceText[1].text = AllEventList[DailyEventIndex].choice2;
        }
    }

    public void choice(int n)
    {
        Text myText = EventText.GetComponent<Text>();
        //choice1Text가 false면 그냥 뜬상태, true면 텍스트가 뜬 상태.
        if (choiceTextBool[n])
        {
            myText.text = null;
            int human = BuildManager.getHuman();
            int money = BuildManager.getMoney();
            int home = BuildManager.getHome();

            for (int i = 0; i < 3; i++)
            {
                EventButton[i].SetActive(false);
            }
            //EventPanel.SetActive(false);
            //EventImage.SetActive(false);
            //결과창때문에 비활함
            ResultButton.SetActive(true);
            if (n == 0)
            {
                myText.text = AllEventList[DailyEventIndex].resultDescription2+"\n";
                if (AllEventList[DailyEventIndex].home1 > 0 )
                {
                    myText.text = myText.text+ "집이 " + AllEventList[DailyEventIndex].home1 + "만큼 늘어났습니다.\n";
                }
                else if(AllEventList[DailyEventIndex].home1 < 0)
                {
                    myText.text = myText.text+ "집이 " + (-AllEventList[DailyEventIndex].home1) + "만큼 부서졌습니다.\n";
                }

                if (AllEventList[DailyEventIndex].human1 > 0)
                {
                    myText.text = myText.text + "신도수가 " + AllEventList[DailyEventIndex].human1 + "만큼 늘어났습니다.\n";
                }
                else if (AllEventList[DailyEventIndex].human1 < 0)
                {
                    myText.text = myText.text + "신도수가 " + (-AllEventList[DailyEventIndex].human1) + "만큼 줄어들었습니다.\n";
                }

                if (AllEventList[DailyEventIndex].money1 > 0)
                {
                    myText.text = myText.text + AllEventList[DailyEventIndex].money1 + "만큼의 돈을 벌었습니다.\n";
                }
                else if (AllEventList[DailyEventIndex].money1 < 0)
                {
                    myText.text = myText.text  + (-AllEventList[DailyEventIndex].money1) + "만큼의 돈을 잃었습니다.\n";
                }


                Debug.Log("신도 변화 = " + AllEventList[DailyEventIndex].human1);
                Debug.Log("돈 변화 = " + AllEventList[DailyEventIndex].money1);
                Debug.Log("집 변화 = " + AllEventList[DailyEventIndex].home1);

                if(human + AllEventList[DailyEventIndex].human1 <= 0)
                {
                    ErrorManager.inst.showError("신도가 모두 사라졌습니다!");
                    BuildManager.buildHumanAction(human);
                }
                else
                    BuildManager.buildHumanAction(-AllEventList[DailyEventIndex].human1);

                if (home + AllEventList[DailyEventIndex].home1 <= 0)
                {
                    ErrorManager.inst.showError("집이 모두 사라졌습니다!");
                    BuildManager.setHome(-home);
                }
                else
                    BuildManager.setHome(AllEventList[DailyEventIndex].home1);

                if (money + AllEventList[DailyEventIndex].money1 <= 0)
                {
                    ErrorManager.inst.showError("돈이 모두 사라졌습니다!");
                    BuildManager.setMoney(-money);
                }
                else
                    BuildManager.setMoney(AllEventList[DailyEventIndex].money1);


            }
            else if (n == 1)
            {
                myText.text = AllEventList[DailyEventIndex].resultDescription2+"\n";
                if (AllEventList[DailyEventIndex].home2 > 0)
                {
                    myText.text = myText.text + "집이 " + AllEventList[DailyEventIndex].home2 + "만큼 늘어났습니다.\n";
                }
                else if (AllEventList[DailyEventIndex].home2 < 0)
                {
                    myText.text = myText.text + "집이 " + (-AllEventList[DailyEventIndex].home2) + "만큼 부서졌습니다.\n";
                }

                if (AllEventList[DailyEventIndex].human2 > 0)
                {
                    myText.text = myText.text + "신도수가 " + AllEventList[DailyEventIndex].human2 + "만큼 늘어났습니다.\n";
                }
                else if (AllEventList[DailyEventIndex].human2 < 0)
                {
                    myText.text = myText.text + "신도수가 " + (-AllEventList[DailyEventIndex].human2) + "만큼 줄어들었습니다.\n";
                }

                if (AllEventList[DailyEventIndex].money2 > 0)
                {
                    myText.text = myText.text + AllEventList[DailyEventIndex].money2 + "만큼의 돈을 벌었습니다.\n";
                }
                else if (AllEventList[DailyEventIndex].money2 < 0)
                {
                    myText.text = myText.text + (-AllEventList[DailyEventIndex].money2) + "만큼의 돈을 잃었습니다.\n";
                }

                Debug.Log("신도 변화 = " + AllEventList[DailyEventIndex].human2);
                Debug.Log("돈 변화 = " + AllEventList[DailyEventIndex].money2);
                Debug.Log("집 변화 = " + AllEventList[DailyEventIndex].home2);

                if (human + AllEventList[DailyEventIndex].human2 <= 0)
                {
                    BuildManager.buildHumanAction(-human);
                    ErrorManager.inst.showError("신도가 모두 사라졌습니다!");
                }
                else
                    BuildManager.buildHumanAction(-AllEventList[DailyEventIndex].human2);

                if (home + AllEventList[DailyEventIndex].home2 <= 0)
                {
                    ErrorManager.inst.showError("집이 모두 사라졌습니다!");
                    BuildManager.setHome(-home);
                }
                else
                    BuildManager.setHome(AllEventList[DailyEventIndex].home2);

                if (money + AllEventList[DailyEventIndex].money2 <= 0)
                {
                    ErrorManager.inst.showError("돈이 모두 사라졌습니다!");
                    BuildManager.setMoney(-money);
                }
                else
                {
                    BuildManager.setMoney(AllEventList[DailyEventIndex].money2);
                }

            }
            else if (n == 2)
            {
                myText.text = AllEventList[DailyEventIndex].resultDescription1+ "\n";
                if (AllEventList[DailyEventIndex].home1 > 0)
                {
                    myText.text = myText.text + "집이 " + AllEventList[DailyEventIndex].home1 + "만큼 늘어났습니다.\n";
                }
                else if (AllEventList[DailyEventIndex].home1 < 0)
                {
                    myText.text = myText.text +"집이 " + (-AllEventList[DailyEventIndex].home1) + "만큼 부서졌습니다.\n";
                }

                if (AllEventList[DailyEventIndex].human1 > 0)
                {
                    myText.text = myText.text + "신도수가 " + AllEventList[DailyEventIndex].human1 + "만큼 늘어났습니다.\n";
                }
                else if (AllEventList[DailyEventIndex].human1 < 0)
                {
                    myText.text = myText.text + "신도수가 " + (-AllEventList[DailyEventIndex].human1) + "만큼 줄어들었습니다.\n";
                }

                if (AllEventList[DailyEventIndex].money1 > 0)
                {
                    myText.text = myText.text + AllEventList[DailyEventIndex].money1 + "만큼의 돈을 벌었습니다.\n";
                }
                else if (AllEventList[DailyEventIndex].money1 < 0)
                {
                    myText.text = myText.text + (-AllEventList[DailyEventIndex].money1) + "만큼의 돈을 잃었습니다.\n";
                }

                Debug.Log("신도 변화 = " + AllEventList[DailyEventIndex].human1);
                Debug.Log("돈 변화 = " + AllEventList[DailyEventIndex].money1);
                Debug.Log("집 변화 = " + AllEventList[DailyEventIndex].home1);

                if (human + AllEventList[DailyEventIndex].human1 <= 0)
                {
                    ErrorManager.inst.showError("신도가 모두 사라졌습니다!");
                    BuildManager.buildHumanAction(human);
                }
                else
                    BuildManager.buildHumanAction(-AllEventList[DailyEventIndex].human1);

                if (home + AllEventList[DailyEventIndex].home1 <= 0)
                {
                    ErrorManager.inst.showError("집이 모두 사라졌습니다!");
                    BuildManager.setHome(-home);
                }
                else
                    BuildManager.setHome(AllEventList[DailyEventIndex].home1);

                if (money + AllEventList[DailyEventIndex].money1 <= 0)
                {
                    ErrorManager.inst.showError("돈이 모두 사라졌습니다!");
                    BuildManager.setMoney(-money);
                }
                else
                    BuildManager.setMoney(AllEventList[DailyEventIndex].money1);
            }

            for(int i=0; i<3;i++)
            {
                choiceBalloon[i].SetActive(false);
                choiceBalloonText[i].SetActive(false);
                choiceTextBool[i] = false;
                ResultButton.SetActive(true);
            }


        }
        else
        {
            choiceTextBool[n] = true;
            choiceBalloon[n].SetActive(true);
            choiceBalloonText[n].SetActive(true);
        }
    }

    public void resultButton()
    {
        EventPanel.SetActive(false);
        EventImage.SetActive(false);
        ResultButton.SetActive(false);
        BackGround.SetActive(false);
        EventTimePause = true;
    }

}
