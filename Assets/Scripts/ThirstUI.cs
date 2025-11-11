using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ThirstUI : MonoBehaviour
{
    bool DEBUG = true;

    int WATER_LEVEL_NONE = 75;
    int WATER_LEVEL_LOW = 150;
    int WATER_LEVEL_NORMAL = 225;

    public Image[] dropletImages;
    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    float deathTimer;

    playerStateManager PlayerState;

    private void Start()
    {
        AssignObjects();
        if (globalVolume != null)
        {
            // Colour adjustments for grayscale
            if (globalVolume.profile.TryGet(out ColorAdjustments ca))
            {
                colorAdjustments = ca;
            }
        }
    }

    void Update()
    {
        DeathByThirst();
        DebugMode();
    }
    private void AssignObjects()
    {
        if (DEBUG)
        {
            OnWaterValueChanged(250);
        }
        PlayerState = GameObject.Find("Player").GetComponent<playerStateManager>();
        if (PlayerState == null)
        {
            //UnityEngine.Debug.LogError("playerStateManager not found in the scene.");
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
        PlayerState.waterDropletLevel = MapToLevel(waterValue);
        UpdateDroplets(PlayerState.waterDropletLevel);
        UpdateEnvironmentGrayscale(PlayerState.waterDropletLevel);
    }

    private int MapToLevel(int waterValue)
    {
        if (waterValue < WATER_LEVEL_NONE) return 0; // Very thirsty - 1 droplet
        if (waterValue < WATER_LEVEL_LOW) return 1; // Thirsty - 2 droplets
        if (waterValue < WATER_LEVEL_NORMAL) return 2; // Normal - 3 droplets
        return 3; // Hydrated - 4 droplets
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
    void DebugMode()
    {
        //for testing purposes only
        if (DEBUG)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                OnWaterValueChanged(WATER_LEVEL_NONE - 10);
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                OnWaterValueChanged(WATER_LEVEL_LOW - 10);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                OnWaterValueChanged(WATER_LEVEL_NORMAL - 10);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                OnWaterValueChanged(250);
                //UnityEngine.Debug.Log("Water Level set to max for testing.");
            }
        }
    }
    void DeathByThirst()
    {
        if (PlayerState.waterDropletLevel == 0)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= 5.0f)
            {
                PlayerState.isDowned = true;
            }
        }
        else
        {
            deathTimer = 0.0f; // Reset timer if not at level 0
        }
    }
}
