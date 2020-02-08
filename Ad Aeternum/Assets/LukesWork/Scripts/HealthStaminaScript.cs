using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStaminaScript : MonoBehaviour
{
    public float value;
    public float icreaseValue;
    public float decreaseValue;
    GameObject gameObj;
    public bool isHealth = false, canSprint, canIncrease = true;
    public StateManager state;

    public GameObject player;

    void Start()
    {
        gameObj = this.gameObject;
        value = gameObj.GetComponent<Slider>().maxValue;
    }

    void Update()
    {
        gameObj.GetComponent<Slider>().value = value;

        if (isHealth == true)
        {
            if (value < gameObj.GetComponent<Slider>().maxValue && canIncrease)
            {
                value += icreaseValue;
            }

            if (value <= 0)
            {
                value = 0;
            }
        }
        else
        {
            if (player.GetComponent<StateManager>().moveAmount > 0 && Input.GetKey(KeyCode.LeftShift))
            {
                if (value > gameObj.GetComponent<Slider>().minValue)
                {
                    value -= decreaseValue;
                }
            }
            else
            {
                if (value < gameObj.GetComponent<Slider>().maxValue && canIncrease)
                {
                    value += icreaseValue;
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || state.moveAmount == 0)
            {
                CancelInvoke();
                canIncrease = false;
                InvokeRepeating("Increase", 2, 2000);
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
