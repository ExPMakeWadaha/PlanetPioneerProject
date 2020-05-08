using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour {
    public static ErrorManager inst;

    public GameObject[] errors = new GameObject[3]; //패널 저장
    int count = 0; //초기 카운트
    public void showError(string message) //텍스트 내용을 지정하여 에러 패널에 띄우는 함수
    {
        if (count < 3){ //처음 입력 3회에 해당
            
                errors[count].SetActive(true);
                count++;
        }
                errors[2].GetComponentInChildren<Text>().text = errors[1].GetComponentInChildren<Text>().text;
                errors[1].GetComponentInChildren<Text>().text = errors[0].GetComponentInChildren<Text>().text;
                errors[0].GetComponentInChildren<Text>().text = message;
    }
	// Use this for initialization
	void Start () {
        /*싱글톤*/
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        for (int i = 0; i < 3; i++)
            errors[i].SetActive(false);

           // showError("신도를 전도와 노동에 투입해 시작하세요.");
        
	}
}
