using UnityEngine;
using UnityEngine.UI;

public class ThirstUI : MonoBehaviour
{
    public Image[] dropletImages;

    [Header("Startup")]
    [Tooltip("What to show before any sensor data arrives")]
    public int defaultWaterValue = 225; // show 4 filled at start

    private void OnEnable()
    {
        UpdateDroplets(MapToLevel(defaultWaterValue));

        PlantGrowth.WaterValueChanged += OnWaterValueChanged;
    }

    private void OnDisable()
    {
        PlantGrowth.WaterValueChanged -= OnWaterValueChanged;
    }

    private void OnWaterValueChanged(int waterValue)
    {
        int level = MapToLevel(waterValue);
        UpdateDroplets(level);
    }

    private int MapToLevel(int waterValue)
    {
        if (waterValue < 75) return 0; // Very thirsty → 1 droplet
        if (waterValue < 150) return 1; // Thirsty → 2 droplets
        if (waterValue < 225) return 2; // Normal → 3 droplets
        return 3;                       // Hydrated → 4 droplets
    }

    private void UpdateDroplets(int level)
    {
        for (int i = 0; i < dropletImages.Length; i++)
        {
            if (dropletImages[i] == null) continue;

            bool filled = i <= level;
            Color c = dropletImages[i].color;
            c.a = filled ? 1f : 0.2f;
            dropletImages[i].color = c;
        }
    }
}
