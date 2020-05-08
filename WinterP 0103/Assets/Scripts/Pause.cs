using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    bool isPause = false;

    public void startPause()
    {
        if (!isPause)
        { //정지상태 아닐 경우
            Time.timeScale = 0;
            isPause = true;
        }
        else //정지상태일 경우
        {
            Time.timeScale = 1;
            isPause = false;
        }
    }
}
