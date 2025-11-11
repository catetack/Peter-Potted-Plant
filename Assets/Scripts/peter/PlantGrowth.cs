using System;
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
    playerStateManager playerState;

    // anyone can subscribe to get the latest water value
    public static event Action<int> WaterValueChanged;

    private void Start()
    {
        if (serialController == null)
            serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    void Update()
    {
        string message = serialController.ReadSerialMessage();
        if (message == null) return;

        UpdatePlantSprite(message);
    }

    void UpdatePlantSprite(string message)
    {
        if (int.TryParse(message, out int waterValue))
        {
            if (waterValue < 75)
                plantRenderer.sprite = plantStage1;
            else if (waterValue < 150)
                plantRenderer.sprite = plantStage2;
            else if (waterValue < 225)
                plantRenderer.sprite = plantStage3;
            else
                plantRenderer.sprite = plantStage4;

            // broadcast the water value for thirst UI
            WaterValueChanged?.Invoke(waterValue);
            playerState.arduinoWaterValue = waterValue;
        }
    }
}
