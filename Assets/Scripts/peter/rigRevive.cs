using System;
using UnityEngine;

public class rigRevive : MonoBehaviour
{
    float reviveLeft;
    float reviveRight;
    InputSystem_Actions inputActions;
    playerStateManager PlayerState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assignObjects();

        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        reviveInput();
        reviveCheck();
        if (PlayerState.isReviving && PlayerState.health < 100.0f)
        {
            PlayerState.health += 40.0f * Time.deltaTime;
        }
        if (PlayerState.health >= 100.0f)
        {
            PlayerState.health = 100.0f;
            PlayerState.isReviving = false;
            PlayerState.isDowned = false;
        }
    }
    void assignObjects()
    {
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
    }
    void reviveInput()
    {
        reviveLeft = inputActions.Player.ReviveLeft.ReadValue<float>();
        reviveRight = inputActions.Player.ReviveRight.ReadValue<float>();
    }
    void reviveCheck()
    {
        if (PlayerState.isDowned)
        {
            if (reviveLeft + reviveRight > 1.9f)
            {
                PlayerState.isReviving = true;
            }
            else
            {
                PlayerState.isReviving = false;
            }
        }
    }
}
