using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//씬변환할려면 꼭 잇어야함

public class RelNameManager : MonoBehaviour {
    public string RelName;
    public Text Nametext;
    private TouchScreenKeyboard keyboard;
    bool NameBool = true;
    public string getName() { return RelName; }
    GameObject Button;
    GameObject SceneButton;
    
    // Use this for initialization

    public void MakingName ()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, true, false);
        //버튼 눌렀을 때 받아오는 함수. 안드로이드 키보드 연다.
    }
    public void SceneChange()
    {
        SceneManager.LoadScene("mainScene");
    }
	// Update is called once per frame
	void Update () {

        if (keyboard != null && keyboard.done && NameBool)
        {
            RelName = keyboard.text;
            NameBool = false;
            Nametext.text = "너의 종교 이름은 " + RelName + "이야! \n 너의 종교를 잘 키워봐! 아래 버튼을 눌러 시작해";
            Button.SetActive(false);
            SceneButton.SetActive(true);
            //이름 띄워주고, 저장하고, 버튼 없앤다.
        }
    }
   void Awake(){
        Button = GameObject.Find("Button");
        SceneButton = GameObject.Find("SceneButton");
        SceneButton.SetActive(false);
        DontDestroyOnLoad (this);
   } 
}
