using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
