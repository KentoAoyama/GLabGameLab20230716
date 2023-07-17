using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class デバッグ用 : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoiGenerateController.Start();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PoiGenerateController.Stop();
        }
    }
}
