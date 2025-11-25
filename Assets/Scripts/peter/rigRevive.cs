using System;
using UnityEngine;

public class rigRevive : MonoBehaviour
{
    float reviveLeft;
    float reviveRight;
    float reviveTimerLeft;
    float reviveTimerRight;
    float reviveThreshold = 0.9f;
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
            PlayerState.health += 60.0f * Time.deltaTime;
        }
        else if (!PlayerState.isReviving && PlayerState.isDowned)
        {
            if (PlayerState.health > 0.0f)
            {
                PlayerState.health -= 20.0f * Time.deltaTime;
            }
            else
            {
                PlayerState.health = 0.0f;
            }
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
            if (reviveLeft > reviveThreshold && reviveTimerLeft <= 1.0f)
            {

                if (reviveRight < reviveThreshold)
                {

                    reviveTimerRight = 0.0f;
                }
                PlayerState.isReviving = true;
                reviveTimerLeft += Time.deltaTime;
            }
            else if (reviveRight > reviveThreshold && reviveTimerRight <= 1.0f)
            {
                if (reviveLeft < reviveThreshold)
                {

                    reviveTimerLeft = 0.0f;
                }
                PlayerState.isReviving = true;
                reviveTimerRight += Time.deltaTime;
            }
            else
            {
                PlayerState.isReviving = false;
            }
        }
    }
}
