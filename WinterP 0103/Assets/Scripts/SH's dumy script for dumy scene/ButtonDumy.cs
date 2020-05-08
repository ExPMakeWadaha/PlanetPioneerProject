using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDumy : MonoBehaviour {
    public GameObject HumanUIPanel;

    bool visible = true;
    public void ChangeUp()
    {
        if (visible)
        {
            HumanUIPanel.SetActive(false);
            visible = false;
        }
        else
        {
            HumanUIPanel.SetActive(true);
            visible = true;
        }
    }

}
