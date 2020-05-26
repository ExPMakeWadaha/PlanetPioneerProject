using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//제이슨으로 데이터를 불러오기 외해서 필요한 클래스이다
//제이슨은 바보여서 List나 array형태로 로드를 받을 수가 없다
//무조건 1개의 클래스를 다운받아야 하기 때문에, 여러개의 데이터를 받으려면 이렇게 해야한다
[System.Serializable]
public class BuildingDataWrapper
{
    public BuildingData[] buildingArr;

}
