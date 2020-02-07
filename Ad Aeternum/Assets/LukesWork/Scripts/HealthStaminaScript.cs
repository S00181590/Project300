using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStaminaScript : MonoBehaviour
{
    float value = 0;
    public float icreaseValue;
    public float decreaseValue;
    GameObject gameObj;
    public bool isHealth = false, canSprint, canIncrease = true;

    public GameObject player;

    void Start()
    {
        gameObj = this.gameObject;
    }

    void Update()
    {
        gameObj.GetComponent<Slider>().value = value;

        if (isHealth == true)
        {
            if (value < gameObj.GetComponent<Slider>().maxValue)
            {
                value += icreaseValue;
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

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                CancelInvoke();
                canIncrease = false;
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

        if (value == 0)
            canIncrease = false;
    }

    void Increase()
    {
        canIncrease = true;
        CancelInvoke();
    }
}
