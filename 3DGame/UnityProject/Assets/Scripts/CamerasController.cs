using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    public GameObject topCamera;
    public GameObject UICamera;

    void Start()
    {
        topCamera.SetActive(false);
        UICamera.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.T))
        {
            topCamera.SetActive(!topCamera.activeSelf);
        }
    }
}
