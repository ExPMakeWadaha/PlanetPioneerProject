using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    public Ray ray;
    public RaycastHit hit; 

    public GameObject target;
    Camera camera;

    void Start()
    {
        camera = Camera.main;
        Debug.Log(camera);
    }

   
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
                target = hit.collider.gameObject;
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            target.transform.Translate(1,0,0);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            target.transform.Translate(-1,0,0);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            target.transform.Translate(0,0,1);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            target.transform.Translate(0,0,-1);
        }
    }
}
