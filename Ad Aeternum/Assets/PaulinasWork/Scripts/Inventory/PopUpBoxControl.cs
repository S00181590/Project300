using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpBoxControl : MonoBehaviour
{
    public Text txtMessage;

    public void InitializePopUp(string message)
    {
        txtMessage.text = message;
    }

    public void ClosePopUp()
    {
        Destroy(gameObject);
    }
}
