using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Data;
using System.Linq;

public class JsonManager
{


    //전체게임 데이터를 저장하는 함수. 
    //게임이 꺼질 때 자동으로 게임매니저에서 호출한다
    public void Save(WholeGameData data)
    {
        //안드로이드에서의 저장 위치를 다르게 해주어야 한다
        //Application.dataPath를 이용하면 어디로 가는지는 구글링 해보길 바란다
        //안드로이드의 경우에는 데이터조작을 막기위해 2진데이터로 변환을 해야한다

        string savePath = Application.dataPath;
        string appender = "/userData/SaveData.json";
#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
        savePath = Application.persistentDataPath;
        
#endif
        //stringBuilder는 최적화에 좋대서 쓰고있다. string+string은 메모리낭비가 심하다
        // 사실 이정도 한두번 쓰는건 상관없긴한데 그냥 써주자. 우리의 컴은 좋으니까..
        StringBuilder builder = new StringBuilder(savePath);
        builder.Append(appender);
        string jsonText = JsonUtility.ToJson(data,true);
        //이러면은 일단 데이터가 텍스트로 변환이 된다
        //jsonUtility를 이용하여 data인 WholeGameData를 json형식의 text로 바꾸어준다
        Debug.Log(jsonText);
        //파일스트림을 이렇게 지정해주고 저장해주면된당 끗
        FileStream fileStream = new FileStream(builder.ToString() , FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }

    public WholeGameData Load()       
    {
        //이제 우리가 이전에 저장했던 데이터를 꺼내야한다
        //만약 저장한 데이터가 없다면? 이걸 실행 안하고 튜토리얼을 실행하면 그만이다. 그 작업은 씬로더에서 해준다
        WholeGameData gameData;
        string loadPath = Application.dataPath;
        string appender = "/userData/SaveData.json";
#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
        loadPath = Application.persistentDataPath;
        
#endif
        StringBuilder builder = new StringBuilder(loadPath);
        builder.Append(appender);
        //위까지는 세이브랑 똑같다
        //파일스트림을 만들어준다. 파일모드를 open으로 해서 열어준다. 다 구글링이다
        FileStream stream = new FileStream(builder.ToString(), FileMode.Open);
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.Close();
        string jsonData = Encoding.UTF8.GetString(bytes);
        Debug.Log(jsonData);
        //텍스트를 string으로 바꾼다음에 FromJson에 넣어주면은 우리가 쓸 수 있는 객체로 바꿀 수 있다
        gameData = JsonUtility.FromJson<WholeGameData>(jsonData);
        return gameData;
        //이 정보를 게임매니저나, 로딩으로 넘겨주는 것이당
    }


    //이거는 빌딩의  데이터를 코드에 손으로 적기가 싫어서 만든 함수이다
    public List<BuildingData> LoadBuildingData()
    {
        //위의 save와 load와는 사뭇 차이가 있다
        //위의 save와 load는 읽기,쓰기를 모두 해야해서 fileStream을 사용했다
        //그렇지만 여기서는 있는 빌딩들의 정보를 읽기만 하면 그만이다.
        //왜냐하면 개인이 바꿀 필요가 없이 개발자만 건드는 정보니까말이다
        //플레이어는 그냥 읽기만 하면 되니, Resources폴더를 이용한다
        BuildingDataWrapper wrapper;
        //wrapper가 뭔지는 wrapper클래스에서 구경하라
        List<BuildingData> data;
        //리턴할 데이터
        BuildingData[] arr;
        //제이슨이 리스트를 못읽는 줄 알고 만든 어레이
        TextAsset text = Resources.Load<TextAsset>("json/buildingData");       
        string dataStr = text.ToString();
        Debug.Log(dataStr);
        wrapper = JsonUtility.FromJson<BuildingDataWrapper>(dataStr);
        //제이슨을 텍스트로변환하고 그걸 객체로 변환한다
        //래퍼 객체로 변환해주고, 래퍼 안의 array만 쏘옥 빼온다
        arr = wrapper.buildingArr;
        data = arr.ToList();
        //그 어레이를 리스트로 바꿔주고 리턴하면 끗
        return data;
    }
}
