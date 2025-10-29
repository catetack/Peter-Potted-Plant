using UnityEngine;
using System.IO.Ports;
using System.Collections.Specialized;
using System.Threading;
using System.Diagnostics;

public class MyListener : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM4", 9600); // Change COM3 to your Arduino's port
    GameObject cubeModifier;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubeModifier = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMessageArrived(string msg)
    {
        float speed = float.Parse(msg) * 100;
        cubeModifier.gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    void OnConnectionEvent(bool success)
    {
        //Debug.Log(success ? "Deviced Connected" : "Device Disconnected");
    }

}
