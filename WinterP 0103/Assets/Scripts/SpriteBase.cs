using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBase : MonoBehaviour {
    public Material mat; //재질
	
    //call when object are created
	void Awake () { 
		gameObject.AddComponent<MeshFilter>(); //메쉬 필터 컴포넌트 불러오기
        gameObject.AddComponent<MeshRenderer>(); //메쉬 렌더러 컴포넌트 불러오기
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear(); //기본 메쉬 지움
        mesh.vertices = new Vector3[]{ //정점 4개 선언, uv좌표 설정
            new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(1.0f, 1.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f)};
        mesh.triangles = new int[] {0,1,2,0,2,3}; //사각형 생성
        /*if (mat != null)
            gameObject.renderer.material = mat;*/
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        MakeResizeByImageSize();
    }
	
	//
	void MakeResizeByImageSize () { //텍스처 이미지만큼 오브젝트 스케일 재조정
        float one_unit = 2.0f/ Screen.height;
        float x = one_unit * mat.mainTexture.width;
        float y = one_unit * mat.mainTexture.height;
        Vector3 newScale = new Vector3(x,y,one_unit);
        transform.localScale = newScale;
    }
}
