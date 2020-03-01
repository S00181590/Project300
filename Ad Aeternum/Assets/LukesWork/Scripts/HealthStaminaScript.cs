using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStaminaScript : MonoBehaviour
{
    public float value;
    float icreaseValue = 3, decreaseValue = 5;

    [HideInInspector]
    public bool canSprint, canIncrease = true;

    StateManager state;
    GameObject player, gameObj;

    void Start()
    {
        state = GameObject.Find("PlayerMoveController").GetComponent<StateManager>();
        player = GameObject.Find("PlayerMoveController");

        gameObj = this.gameObject;
        value = gameObj.GetComponent<Slider>().maxValue;
    }

    void Update()
    {
        gameObj.GetComponent<Slider>().value = value;

        if (gameObject.name == "PlayerHealthSlider") //Health
        {
            if (value < gameObj.GetComponent<Slider>().maxValue && canIncrease && Time.timeScale != 0)
            {
                value += icreaseValue;
            }

            if (value <= 0)
            {
                value = 0;
            }
        }
        else //Stamina
        {
            if (state.moveAmount > 0 && state.onGround)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    CancelInvoke();
                    canIncrease = false;
                    value -= decreaseValue;
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || state.moveAmount == 0)
            {
                InvokeRepeating("Increase", 2, 2000);
            }

            if (value < gameObj.GetComponent<Slider>().maxValue && canIncrease && Time.timeScale != 0)
            {
                value += icreaseValue;
            }

            if (state.controllerSprint == true)
            {
                canIncrease = false;
                value -= decreaseValue;
            }
            else
            {
                InvokeRepeating("Increase", 2, 2000);
            }
        }

        if (value > 0)
        {
            canSprint = true;
        }
        else
        {
            canSprint = false;
        }
    }

    void Increase()
    {
        canIncrease = true;
        CancelInvoke();
    }
}
