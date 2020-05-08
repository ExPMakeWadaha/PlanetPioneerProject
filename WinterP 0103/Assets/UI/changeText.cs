using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeText : MonoBehaviour {
    int before; //이전 변수값  
    int num; //차이값
    Text text;
    float timer;
    int waiting;

	// Use this for initialization
	void Start () {
        timer = 0.0f;
        waiting = 3;

        text = GetComponent<Text>();
        text.text = "+"+BuildManager.getHuman().ToString();
        before = BuildManager.getHuman();
	}
	
	// Update is called once per frame
	void Update () {
        num = BuildManager.getHuman() - before;
        if (num != 0)
        {
            timer = 0.0f;
            text.text = num.ToString();
            if (num >= 0) //차이값이 양수일 경우
            {
                text.text = "+" + num.ToString();
                text.color = new Color(0.0f, 1.0f, 0.0f);

            }
            else //차이값이 음수일 경우
            {
                text.color = new Color(1.0f, 0.0f, 0.0f);
            }

            before = BuildManager.getHuman();
        }
        else //변동이 없을 때 들어감
        {
            timer += Time.deltaTime;
            if (timer > waiting)
            {
                text.text = " ";
            }
        }
	}
}
