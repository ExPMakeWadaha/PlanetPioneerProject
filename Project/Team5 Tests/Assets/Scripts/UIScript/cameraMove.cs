using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float cameraSpeed;   //카메라가 움직이는 속도
    public static Camera cam;   //메인 카메라
    // Start is called before the first frame update
    public RayScript rayScript;
    bool isBuildingTouched;

    Vector2 nowPos, prePos;  //now는 현재, pre는 기록된
    Vector2 nowZoomPos, preZoomPos;
    Vector3 movePos;
    float nowZoomMag, preZoomMag;
    float deltaZoomMag;

    void Start()
    {
        cam = Camera.main;
        isBuildingTouched = false;
        nowZoomMag = 0;
        preZoomMag = 0;
        deltaZoomMag = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 1) // 터치가 되었다면,
        {

            if (isBuildingTouched)
            {
                //빌딩옮기는 중이면 예외처리
                return;
            }
            Vector3 rotatePos;
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) // 터치 페이즈 
            {
                prePos = touch.position;  //터치의 이전 좌표 기록
            }
            else if (touch.phase == TouchPhase.Moved)    //터치하고 움직인다면,
            {
                //여기는 무빙
                nowPos = touch.position;  //움직인만큼 좌표 입력
                rotatePos = new Vector3(touch.deltaPosition.y, -1*touch.deltaPosition.x, 0);
                rotatePos *= 0.1f;
                cam.transform.Rotate(rotatePos);
                cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, 0);
                //cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.x, cam.transform.rotation.y, 0);
                prePos = touch.position;  // 다시 계산하기 위해 이전좌표 재설정
            }


        }

        if (Input.touchCount == 2)
        {
            if (isBuildingTouched)
            {
                //빌딩옮기는 중이면 예외처리
                return;
            }

            Touch touch = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            
            //카메라 자체 이동
            if (touch.phase == TouchPhase.Began) // 터치 페이즈 
            {
                //무빙
                prePos = touch.position;  //터치의 이전 좌표 기록

                //확대축소
                preZoomPos = touch.position - touch2.position;   //줌땡기려고
                preZoomMag = preZoomPos.magnitude;
            }
            else if (touch.phase == TouchPhase.Moved)    //터치하고 움직인다면,
            {

                bool isZoomed = false;
                //여기는 확대축소
                nowZoomPos = touch.position - touch2.position;
                nowZoomMag = nowZoomPos.magnitude;
                deltaZoomMag = preZoomMag - nowZoomMag;
                deltaZoomMag *= 0.5f;
                if (Mathf.Abs(deltaZoomMag) > 4.0f)
                {
                    Debug.Log("줌땡기고있어 " + deltaZoomMag);
                    isZoomed = true;
                    cam.fieldOfView += deltaZoomMag;
                    if (cam.fieldOfView > 120)
                    {
                        cam.fieldOfView = 120;
                    }
                    else if(cam.fieldOfView < 40)
                    {
                        cam.fieldOfView = 40;
                    }
                }
                preZoomPos = touch.position - touch2.position;   //줌땡기려고
                preZoomMag = preZoomPos.magnitude;

                if (isZoomed)
                {
                    return;
                }
                //여기는 무빙
                nowPos = touch.position;  //움직인만큼 좌표 입력
                movePos = (Vector3)(prePos - nowPos) * cameraSpeed; //움직인만큼 벡터로 변환
                cam.transform.Translate(movePos);   //움직여줌
                preZoomPos = nowZoomPos;
                prePos = touch.position;  // 다시 계산하기 위해 이전좌표 재설정




            }
        }
    }

    public void BuildingTouched(bool touched)
    {
        isBuildingTouched = touched;
    }
}
