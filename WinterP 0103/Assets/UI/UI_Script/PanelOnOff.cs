using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOnOff : MonoBehaviour
{ //빈 부모스크립트에 들어감
    public void panelOn()
    {
        gameObject.SetActive(true);
    }
    public void panelOff()
    {
        gameObject.SetActive(false);
    }
}
