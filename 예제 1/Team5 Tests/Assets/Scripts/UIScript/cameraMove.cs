using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float cameraSpeed;   //카메라가 움직이는 속도
    public static Camera cam;   //메인 카메라
    // Start is called before the first frame update

    public Vector2 nowPos, prePos;  //now는 현재, pre는 기록된
    public Vector3 movePos;     //움직인 벡터

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.touchCount == 1) // 터치가 되었다면,
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began) // 터치 페이즈 
            {
                prePos = touch.position - touch.deltaPosition;  //터치의 이전 좌표 기록
            }
            else if(touch.phase == TouchPhase.Moved)    //터치하고 움직인다면,
            {
                nowPos = touch.position - touch.deltaPosition;  //움직인만큼 좌표 입력
                movePos = (Vector3)(prePos - nowPos) * cameraSpeed; //움직인만큼 벡터로 변환
                cam.transform.Translate(movePos);   //움직여줌
                prePos = touch.position - touch.deltaPosition;  // 다시 계산하기 위해 이전좌표 재설정
            }
            else if(touch.phase == TouchPhase.Ended) // 터치가 끝남
            {
            }
        }
    }
}
