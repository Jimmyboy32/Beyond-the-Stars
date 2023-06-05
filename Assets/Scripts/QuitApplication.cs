using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        CloseApplication();
    }

    void CloseApplication()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quitting application");
            Application.Quit();
        }
    }
}


 