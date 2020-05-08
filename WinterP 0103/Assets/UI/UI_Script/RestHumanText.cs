using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestHumanText : MonoBehaviour {
    int restHuman;
    Text restHumanLabel;

    // Use this for initialization
    void Awake()
    {
        restHumanLabel = GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        restHuman = BuildManager.getHuman() - BuildManager.getMakeHuman() - BuildManager.getMakeMoney();
        restHumanLabel.text = restHuman.ToString();
    }
}
