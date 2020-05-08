using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeMoneyText : MonoBehaviour {

    int make_money;
    Text makeMoneyLabel;

    // Use this for initialization
    void Awake()
    {
        makeMoneyLabel = GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        make_money = BuildManager.getMakeMoney();
        makeMoneyLabel.text = make_money.ToString();
    }
}
