using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButton2 : MonoBehaviour {
    public GameObject Panel;
    public static bool onoff2 = false;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void action()
    {
        if (onoff2 == false) //꺼져있으면 켠다
        {
            if (MainButton.onoff1 == true) //꺼져있는데 다른거 켜져있으면 다른거 끄고 켠다
            {
                Panel.SetActive(false);
                MainButton.onoff1 = false;
            }
            gameObject.SetActive(true);
            onoff2 = true;
        }
        else //켜져있으면 끈다
        {
            gameObject.SetActive(false);
            onoff2 = false;
        }
    }
}
