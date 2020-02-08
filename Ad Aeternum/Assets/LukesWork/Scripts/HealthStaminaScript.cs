using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStaminaScript : MonoBehaviour
{
    public float value;
    float icreaseValue = 3;
    float decreaseValue = 5;
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
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    if (state.moveAmount == 0)
            //    {
            //        CancelInvoke();
            //        InvokeRepeating("Increase", 2, 2000);
            //    }
            //    else
            //    {
            //        if (value > gameObj.GetComponent<Slider>().minValue)
            //        {
            //            canIncrease = false;
            //            value -= decreaseValue;
            //        }
            //    }
            //}

            if (state.moveAmount > 0)
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

            if (value < gameObj.GetComponent<Slider>().maxValue && canIncrease)
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
