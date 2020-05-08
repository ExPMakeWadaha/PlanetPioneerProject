using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RonnieJ.Coroutine
{
    public class FadeOut : MonoBehaviour {
    
	    public float animTime = 1f;
        Image fadeImage;
        float start = 0f; float end = 1f; float time = 0f;
        bool isPlaying = false;

        void Awake()
        {
            fadeImage = GetComponent<Image>();	
        }

	    public void StartFadeAnim () //call할 함수
        {
            if (isPlaying == true) return;
            StartCoroutine("playFadeOut");
	    }

        IEnumerator playFadeOut()
        {
            isPlaying = true;

            Color color = fadeImage.color;
            time = 0f;
            color.a = Mathf.Lerp(start, end, time);

            while (color.a < 1f)
            {
                //경과시간 계산
                time += Time.deltaTime / animTime;
                color.a = Mathf.Lerp(start, end, time);
                fadeImage.color = color;

                yield return null;
            }
            isPlaying = false;
        }
    }
}

//호출조건: 1초동안 에러함수가 call되지 않을 때
//애니메이션이 진행중이어도 에러함수가 call되면 즉시 취소되어야 함 - 이게 어려움. call시간이 초기화될 시, 취소되어야하는데...음 (애니메이션 중단 검색)
//에러함수가 call되는걸 어떻게 아냐??? - 가장 최근에 call한 시간을 기록, 1초를 재고(경과시간 계산) 발동
//패널 3개는 동시에 fadeout (errors[0-3]이니까 매니저에서 하면됨)