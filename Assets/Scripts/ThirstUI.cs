using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ThirstUI : MonoBehaviour
{
    // Assign 4 Image components (droplet icons) in the Inspector
    public Image[] dropletImages;

    private void OnEnable()
    {
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

    // Map water value into 4 “levels”
    private int MapToLevel(int waterValue)
    {
        // Returns an index 0–3 (0 = very thirsty, 3 = hydrated)
        if (waterValue < 75) return 0; // Very thirsty → 1 droplet
        if (waterValue < 150) return 1; // Thirsty → 2 droplets
        if (waterValue < 225) return 2; // Normal → 3 droplets
        return 3;                       // Hydrated → 4 droplets
    }

    private void UpdateDroplets(int level)
    {
        // Level is 0..3 meaning number of droplets to “fill”
        // We treat each droplet as filled/empty by adjusting alpha

        for (int i = 0; i < dropletImages.Length; i++)
        {
            if (dropletImages[i] == null) continue;

            bool filled = i <= level;
            dropletImages[i].color = new Color(1f, 1f, 1f, filled ? 1f : 0.2f);
        }
    }
}
