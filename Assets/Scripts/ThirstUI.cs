using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class ThirstUI : MonoBehaviour
{
    public Image[] dropletImages;
    public Volume globalVolume; // Assign your Global Volume here

    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        if (globalVolume != null)
        {
            // Try to get ColorAdjustments from the Volume
            if (globalVolume.profile.TryGet(out ColorAdjustments ca))
            {
                colorAdjustments = ca;
            }
            else
            {
                //Debug.LogWarning("No ColorAdjustments found in the Volume!");
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
        if (waterValue < 75) return 0;
        if (waterValue < 150) return 1;
        if (waterValue < 225) return 2;
        return 3;
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

        // Convert thirst level (0–3) into saturation (0 to -100)
        // 3 = hydrated → full color
        // 0 = dying → full grayscale
        float saturation = Mathf.Lerp(-100f, 0f, level / 3f);

        colorAdjustments.saturation.Override(saturation);
    }
}
