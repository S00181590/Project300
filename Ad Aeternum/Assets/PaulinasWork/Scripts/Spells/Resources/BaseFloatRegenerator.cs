using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFloatRegenerator : MonoBehaviour
{
    public float Value;
    public float MaxValue;
    public float RegenRate;

    private void Update()
    {
        if(Value<MaxValue)
        {
            Value += RegenRate * Time.deltaTime;

            if (Value > MaxValue)
                Value = MaxValue;
        }
    }
}
