using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour {
    int money;
    Text moneyLabel;

    // Use this for initialization
    void Awake()
    {
        moneyLabel = GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        money = BuildManager.getMoney();
        moneyLabel.text = money.ToString();
    }
}
