using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeHumanText : MonoBehaviour {

    int make_human;
    Text makeHumanLabel;

    // Use this for initialization
    void Awake()
    {
        makeHumanLabel = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        make_human = BuildManager.getMakeHuman();
        makeHumanLabel.text = make_human.ToString();
    }

}
