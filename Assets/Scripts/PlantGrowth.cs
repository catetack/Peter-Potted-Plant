using System.Diagnostics;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public SerialController serialController;
    public SpriteRenderer plantRenderer;
    public Sprite plantStage1;
    public Sprite plantStage2;
    public Sprite plantStage3;
    public Sprite plantStage4;


    private void Start()
    {
        if (serialController == null)
            serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    void Update()
    {
        string message = serialController.ReadSerialMessage();
        if (message == null) return;

        //// Handle possible connection messages from Ardity
        //if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
        //    Debug.Log("Arduino connected");
        //else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
        //    Debug.Log("Arduino disconnected");
        //else
            UpdatePlantSprite(message);
    }

    void UpdatePlantSprite(string message)
    {
        int waterValue;
        if (int.TryParse(message, out waterValue))
        {
            if (waterValue < 50)
                plantRenderer.sprite = plantStage1;
            else if (waterValue < 100)
                plantRenderer.sprite = plantStage2;
            else if (waterValue < 150)
                plantRenderer.sprite = plantStage3;
            else
                plantRenderer.sprite = plantStage4;
        }
    }
}
