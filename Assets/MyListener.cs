using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MyListener : MonoBehaviour
{
    GameObject cubeModifier;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubeModifier = GameObject.Find("pHead_0");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("cheeseburger");
    }

    void OnMessageArrived(string msg)
    {
        float speed = float.Parse(msg) * 100;
        cubeModifier.gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
