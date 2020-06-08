using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    Ray ray;
    RaycastHit hit; 

    GameObject target;              //퍼블릭 빼준당
    public GameManager gameManager; //게임매니저에 정보를 넘겨줘야한다.
    Camera camera;
    Vector3 center;
    const string objectTag = "Building";  //게임오브젝트의 태그다. 고정이니까 const
    void Start()
    {
        center = new Vector3(Screen.currentResolution.width/2, Screen.currentResolution.height/2,0);
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
                    return;
                }
            }
        }
        /*
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
        */
         if (Input.GetMouseButton(0))
        {
            if(target == null)
            {
                return;
            }

            target.transform.position = ScreenToWorld();

            //좌클릭을 누르는 동안 마우스 좌표를 받아와 월드좌표로 변환 후, 타겟을 마우스의 위치로 옮깁니다.

        }

        if (Input.GetMouseButtonUp(0))
        {
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
        Vector3 clickPoint = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Sqrt(2000)));
        float root1200 = Mathf.Sqrt(1200);
        float x = -1 * 20 / (clickPoint.y - 20) * (clickPoint.x - root1200) + root1200;
        float z = -1 * 20 / (clickPoint.y - 20) * (clickPoint.z + 20) - 20;

        /*그리드는 xz평면 위에있다. 그래서 y=0이어야 한다
 * 그렇지만 카메라는 비스듬히 놓여져 있으므로, 카메라에서 터치하는 값을
 * 변환해준 값은 y=0이 나오지 않는다 만들어줄 수 없다
 * 그렇다고 해서 강제로 y=0으로 하면 클릭좌표와 오브젝트 좌표가 같아보이지 않아 이질감이 생긴다
 * 그걸 해소하기 위한 x,z을 새로 설정해준다
 * 클릭포인트의 좌표벡터 l벡터, x1,y1,z1이라고 놓고, 카메라의 좌표벡터를 n벡터라고 놓자.
 * n벡터와 l벡터를 지나가는 직선을 구하고, 그 직선의 y좌표가 0일때의 x,z를 구한다
 * 그게 위의 것이다.
 */


        x = Mathf.RoundToInt(x);
        z = Mathf.RoundToInt(z);


        //반올림까지 여기서 해부리자
        Vector3 pos = new Vector3(x, 0, z);

        return pos;

    }

}
