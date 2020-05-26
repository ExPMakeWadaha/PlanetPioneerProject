using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    public Ray ray;
    public RaycastHit hit; 

    public GameObject target;
    Camera camera;

    void Start()
    {
        camera = Camera.main; // 메인카메라를 호출합니다. Start에 넣어야 작동하더라구요
    }

   
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            ray = camera.ScreenPointToRay(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
                target = hit.collider.gameObject; //Ray에 맞은 콜라이더를 타겟으로 설정
            }
        }
        if(Input.GetMouseButtonDown(1)){
            target = null; // 마우스 우클릭으로 선택해제
        }
        
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            target.transform.Translate(-1,0,0); // 왼쪽 화살표 누를 시 왼쪽으로 이동
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            target.transform.Translate(1,0,0); // 오른쪽 화살표 누를 시 오른쪽으로 이동
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            target.transform.Translate(0,0,1); // 위쪽 화살표 누를 시 위쪽으로 이동 
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            target.transform.Translate(0,0,-1); // 아래쪽 화살표 누를 시 아래쪽으로 이동 
        }
         if (Input.GetMouseButton(0))
        {
            Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15f);
            pos = camera.ScreenToWorldPoint(pos);
            //pos.y = 0;
            target.transform.position = pos;
            Debug.Log(Input.mousePosition + " = mouse");
            Debug.Log(camera.ScreenToWorldPoint(pos) + " = screenToWorld");
            //좌클릭을 누르는 동안 마우스 좌표를 받아와 월드좌표로 변환 후, 타겟을 마우스의 위치로 옮깁니다.

        }

        if (Input.GetMouseButtonUp(0))
        {
            
            Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20f);
            pos = camera.ScreenToWorldPoint(pos);
            pos.x = Mathf.RoundToInt(pos.x);
            pos.y = Mathf.RoundToInt(pos.y);
            pos.z = Mathf.RoundToInt(pos.z);
            target.transform.position = pos;
            //마우스 좌클릭을 해제하면 받아온 좌표를 반올림하여 타겟의 좌표로 설정
            
        }
    }

}
