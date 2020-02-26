using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignScript : MonoBehaviour
{
    GameObject popUpBox;
    Camera cam;
    Text signReadText = null, signText = null;
    bool signActive = false;
    public string textOnSign = "";

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        signReadText = GameObject.Find("SignReadText").GetComponent<Text>();
        signText = GameObject.Find("SignText").GetComponent<Text>();
        popUpBox = GameObject.Find("PopUpBox");
        popUpBox.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactive")
        {
            signReadText.transform.position = cam.WorldToScreenPoint(transform.position + new Vector3(0, 2.8f, 0));
            signReadText.text = "Press F To Read Sign";
            signReadText.enabled = true;

            if (Input.GetKeyDown(KeyCode.F) || (Input.GetKeyDown(KeyCode.Joystick1Button0)))
            {
                //signText = this.GetComponentInChildren<Text>();
                signActive = !signActive;
            }

            if (signActive)
            {
                signText.text = textOnSign.ToString();
                popUpBox.SetActive(true);
                //signText.enabled = true;
            }
            else
            {
                popUpBox.SetActive(false);
                //signText.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactive")
        {
            signReadText.enabled = false;
            signActive = false;
            popUpBox.SetActive(false);
        }
    }
}
