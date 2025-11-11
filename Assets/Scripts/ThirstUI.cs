using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ThirstUI : MonoBehaviour
{
    public Image[] dropletImages;
    public Volume globalVolume;

    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        if (globalVolume != null)
        {
            // Colour adjustments for grayscale
            if (globalVolume.profile.TryGet(out ColorAdjustments ca))
            {
                colorAdjustments = ca;
            }
        }
    }

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
        UpdateEnvironmentGrayscale(level);
    }

    private int MapToLevel(int waterValue)
    {
        if (waterValue < 75) return 0; // Very thirsty → 1 droplet
        if (waterValue < 150) return 1; // Thirsty → 2 droplets
        if (waterValue < 225) return 2; // Normal → 3 droplets
        return 3; // Hydrated → 4 droplets
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

    private void UpdateEnvironmentGrayscale(int level)
    {
        if (colorAdjustments == null) return;

        // Saturation
        // 3 = full color
        // 0 = grayscale
        float saturation = Mathf.Lerp(-100f, 0f, level / 3f);

        colorAdjustments.saturation.Override(saturation);
    }
}
