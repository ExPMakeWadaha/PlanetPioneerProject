using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButton : MonoBehaviour {
    public GameObject Panel;
    public static bool onoff1 = false;
    void Awake()
    {
        gameObject.SetActive(false);
    }
    public void action(){  
        if (onoff1 == false) //꺼져있으면 켠다
        {
            if (MainButton2.onoff2 == true) //꺼져있는데 다른거 켜져있으면 다른거 끄고 켠다
            {
                Panel.SetActive(false);
                MainButton2.onoff2 = false;
            }
            gameObject.SetActive(true);
            onoff1 = true;
        }
        else //켜져있으면 끈다
        {
            gameObject.SetActive(false);
            onoff1 = false;
        }
    }
}
