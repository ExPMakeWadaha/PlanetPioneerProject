using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitTest : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 vector;
    Vector3 x, y, z;
    void Start()
    {
        vector = new Vector3(0,0,0);
        x = new Vector3(1, 0, 0);
        y = new Vector3(0, 1, 0);
        z = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            vector += x;
            transform.position = vector;
        }
        if (Input.GetKey(KeyCode.F2))
        {
            vector += y;
            transform.position = vector;
        }
        if (Input.GetKey(KeyCode.F3))
        {
            vector += z;
            transform.position = vector;
        }

    }
}
