using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutHumanUI : MonoBehaviour {
    GameObject myPanel;
    Slider MakeHumanSlider;
    Slider SubHumanSlider;
    Slider MakeMoneySlider;
    Slider SubMoneySlider;

    GameObject BackGround;

    GameObject MakeHuman;
    GameObject SubHuman;
    GameObject MakeMoney;
    GameObject SubMoney;

    GameObject MakeHumanSprite;
    GameObject SubHumanSprite;
    GameObject MakeMoneySprite;
    GameObject SubMoneySprite;


    GameObject myValue;
    Text myValueText;

    bool[] ActiveArray = new bool[5];
    int SliderCase;
    private void Awake()
    {
        myPanel = GameObject.Find("MakingPanel");
        BackGround = GameObject.Find("BackGround");

        MakeHumanSlider = GameObject.Find("MakeHumanSlider").GetComponent<Slider>();
        SubHumanSlider = GameObject.Find("SubHumanSlider").GetComponent<Slider>();
        MakeMoneySlider = GameObject.Find("MakeMoneySlider").GetComponent<Slider>();
        SubMoneySlider = GameObject.Find("SubMoneySlider").GetComponent<Slider>();

        //slider다
        MakeHuman = GameObject.Find("MakeHumanSlider");
        SubHuman = GameObject.Find("SubHumanSlider");
        MakeMoney = GameObject.Find("MakeMoneySlider");
        SubMoney = GameObject.Find("SubMoneySlider");

        MakeHumanSprite = GameObject.Find("MakeHumanSprite");
        SubHumanSprite = GameObject.Find("SubHumanSprite");
        MakeMoneySprite = GameObject.Find("MakeMoneySprite");
        SubMoneySprite = GameObject.Find("SubMoneySprite");

        myValue = GameObject.Find("myValue");
        myValueText = myValue.GetComponent<Text>();

        myPanel.SetActive(false);
        BackGround.SetActive(false);

        MakeHuman.SetActive(false);
        SubHuman.SetActive(false);
        MakeMoney.SetActive(false);
        SubMoney.SetActive(false);

        MakeHumanSprite.SetActive(false);
        SubHumanSprite.SetActive(false);
        MakeMoneySprite.SetActive(false);
        SubMoneySprite.SetActive(false);


    }

    //버튼이 눌리면 슬라이더가 뜨고, 조종하면된다
    //슬라이더 어느거 띄울지 고르는 함수
    /*버튼 눌림 함수, 어느 슬라이드가 액티브될지 설정*/
    public void Button(int i)
    {
        EventManager.EventTimePause = false;
        myPanel.SetActive(true);
        BackGround.SetActive(true);
        switch (i)
        {
            case 1: MakeHuman.SetActive(true);
                MakeHumanSprite.SetActive(true);
                SliderCase = 1;
                MakeHumanSlider.maxValue = BuildManager.getHuman() - BuildManager.getMakeHuman() - BuildManager.getMakeMoney();
                myValueText.text = "+0";
                //전도투입
                break;

            case 2: SubHuman.SetActive(true);
                SubHumanSprite.SetActive(true);
                SliderCase = 2;
                SubHumanSlider.maxValue = BuildManager.getMakeHuman();
                //전도차출
                myValueText.text = "-0";
                break;

            case 3: MakeMoney.SetActive(true);
                MakeMoneySprite.SetActive(true);
                SliderCase = 3;
                MakeMoneySlider.maxValue = BuildManager.getHuman() - BuildManager.getMakeHuman() - BuildManager.getMakeMoney();
                myValueText.text = "+0";
                //노동투입
                break;

            case 4:SubMoney.SetActive(true);
                SubMoneySprite.SetActive(true);
                SliderCase = 4;
                SubMoneySlider.maxValue = BuildManager.getMakeMoney();
                myValueText.text = "-0";
                //노동차출
                break;
        }
    }

    


    /*전도 투입,차출 함수*/

    public void MakeHumanSliderUpdate()
    {
        myValueText.text = "+" + ((int)MakeHumanSlider.value).ToString();
    }

    public void SubHumanSliderUpdate()
    {
        myValueText.text = "-" + ((int)SubHumanSlider.value).ToString();

    }


    /*노동 투입,차출 함수*/
    public void MakeMoneySliderUpdate()
    {
        myValueText.text = "+"+((int)MakeMoneySlider.value).ToString();
    }
    public void SubMoneySliderUpdate()
    {
        myValueText.text = "-"+((int)SubMoneySlider.value).ToString();

    }

    /*창 끄는 함수*/
    public void ExitPanel()
    {
        switch (SliderCase)
        {
            case 1:
                BuildManager.addMakeHuman((int)MakeHumanSlider.value);
                MakeHumanSlider.value = 0;
                MakeHumanSprite.SetActive(false);
                break;

            case 2:
                BuildManager.subMakeHuman((int)SubHumanSlider.value);
                SubHumanSlider.value = 0;
                SubHuman.SetActive(false);
                SubHumanSprite.SetActive(false);
                break;

            case 3:
                BuildManager.addMakeMoney((int)MakeMoneySlider.value);
                MakeMoneySlider.value = 0;
                MakeMoney.SetActive(false);
                MakeMoneySprite.SetActive(false);
                break;

            case 4:
                BuildManager.subMakeMoney((int)SubMoneySlider.value);
                SubMoneySlider.value = 0;
                SubMoney.SetActive(false);
                SubMoneySprite.SetActive(false);
                break;
        }
        EventManager.EventTimePause = true;
        myPanel.SetActive(false);
        BackGround.SetActive(false);
    }

    public void MaxButton()
    {
        switch (SliderCase)
        {
            case 1:
                BuildManager.addMakeHuman((int)MakeHumanSlider.value);
                MakeHumanSlider.value = MakeHumanSlider.maxValue;
                break;

            case 2:
                BuildManager.subMakeHuman((int)SubHumanSlider.value);
                SubHumanSlider.value = SubHumanSlider.maxValue;
                break;

            case 3:
                BuildManager.addMakeMoney((int)MakeMoneySlider.value);
                MakeMoneySlider.value = MakeMoneySlider.maxValue;
                break;

            case 4:
                BuildManager.subMakeMoney((int)SubMoneySlider.value);
                SubMoneySlider.value = SubMoneySlider.maxValue;
                break;
        }
    }

    public void NoButton()
    {
            switch (SliderCase)
            {
                case 1:
                    MakeHumanSlider.value = 0;
                    MakeHuman.SetActive(false);
                MakeHumanSprite.SetActive(false);
                break;

                case 2:

                    SubHumanSlider.value = 0;
                    SubHuman.SetActive(false);
                SubHumanSprite.SetActive(false);
                break;

                case 3:

                    MakeMoneySlider.value = 0;
                    MakeMoney.SetActive(false);
                MakeMoneySprite.SetActive(false);
                break;

                case 4:

                    SubMoneySlider.value = 0;
                    SubMoney.SetActive(false);
                SubMoneySprite.SetActive(false);
                break;
            }
        EventManager.EventTimePause = true;
        myPanel.SetActive(false);
        BackGround.SetActive(false);
    }

}

