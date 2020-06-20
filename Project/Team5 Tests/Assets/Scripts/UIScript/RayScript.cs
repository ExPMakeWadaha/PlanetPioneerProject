using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    Ray ray;
    RaycastHit hit; 

    GameObject target;              //퍼블릭 빼준당
    public GameManager gameManager; //게임매니저에 정보를 넘겨줘야한다.
    Camera camera;
    const string objectTag = "Building";  //게임오브젝트의 태그다. 고정이니까 const
    public cameraMove cameraMover;
    Building targetBuilding;
    //building object that now player is clicking

    //200620 new script by sanghub
    /// <summary>
    /// i'm gonna make boundary of buildings
    /// thats it
    /// </summary>

    const int negativeBound = -15;
    const int positiveBound = 15;
   

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
                if (!target.CompareTag(objectTag))
                {
                    target = null;
                    cameraMover.BuildingTouched(false);
                    return;
                }
                else
                {
                    cameraMover.BuildingTouched(true);
                    targetBuilding = gameManager.FindBuilding(target);
                    //you save targetbuilding on buttonDown
                    //so you can give Building to GameManager on button an buttonUp

                }
            }
        }
         if (Input.GetMouseButton(0))
        {
            if(target == null)
            {
                return;
            }
            Vector3 pos = ScreenToWorld();
            //we have to process this with gameManager
            //because gameManager has BuildingList, so you can know collidings
            target.transform.position = gameManager.OnBuildingCollision(targetBuilding, pos);

            //좌클릭을 누르는 동안 마우스 좌표를 받아와 월드좌표로 변환 후, 타겟을 마우스의 위치로 옮깁니다.

        }

        if (Input.GetMouseButtonUp(0))
        {
            cameraMover.BuildingTouched(false);
            if (target == null)
            {
                return;
            }
            Debug.Log("마우스 버튼 업");
            Vector3 positionVector = ScreenToWorld();
            target.transform.position = positionVector;
            gameManager.ChangeBuildingPosition(target);
            target = null;
            //이제 타겟을 널로 만들어줍니다.
            
        }
    }

    Vector3 ScreenToWorld()
    {
        //Vector3 normalCameraVector = new Vector3(Mathf.Sqrt(0.6f), Mathf.Sqrt(0.2f), -Mathf.Sqrt(0.2f));
        Vector3 cameraPos = camera.transform.position;
        Vector3 cameraToXZ = new Vector3(cameraPos.x - 1.5f * cameraPos.y, 0, cameraPos.z - Mathf.Sqrt(0.75f) * cameraPos.y);
        //저기 이 부분에서 지금 고정값이 들어가있는데, 이게 들어가있는데도 왜 잘되는지 모르겠어요. 이거 원래 되면 안되거등요

        float cameraSqrMag = (cameraPos - cameraToXZ).magnitude;
        
        
        Vector3 clickPoint = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraSqrMag));

        float x = -1 * cameraPos.y / (cameraPos.y - clickPoint.y) * (cameraPos.x - clickPoint.x) + cameraPos.x;
        float z = -1 * cameraPos.y / (cameraPos.y - clickPoint.y) * (cameraPos.z - clickPoint.z) + cameraPos.z;


        /*그리드는 xz평면 위에있다. 그래서 y=0이어야 한다
 * 그렇지만 카메라는 비스듬히 놓여져 있으므로, 카메라에서 터치하는 값을
 * 변환해준 값은 y=0이 나오지 않는다 만들어줄 수 없다
 * 그렇다고 해서 강제로 y=0으로 하면 클릭좌표와 오브젝트 좌표가 같아보이지 않아 이질감이 생긴다
 * 그걸 해소하기 위한 x,z을 새로 설정해준다
 * 클릭포인트의 좌표벡터 l벡터, x1,y1,z1이라고 놓고, 카메라의 좌표벡터를 n벡터라고 놓자.
 * n벡터와 l벡터를 지나가는 직선을 구하고, 그 직선의 y좌표가 0일때의 x,z를 구한다
 * 그게 위의 것이다.
 */


        //반올림까지 여기서 해부리자
        x = Mathf.RoundToInt(x);
        z = Mathf.RoundToInt(z);


        //this is for boundary check
        if(x> positiveBound)
        {
            x = positiveBound;
        }
        else if (x < negativeBound)
        {
            x = negativeBound;
        }
        if (z > positiveBound)
        {
            z = positiveBound;
        }
        else if (z < negativeBound)
        {
            z = negativeBound;
        }
        Vector3 pos = new Vector3(x, 0, z);

        return pos;

    }

}
