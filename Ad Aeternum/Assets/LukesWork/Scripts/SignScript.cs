using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignScript : MonoBehaviour
{
    Camera cam;
    GameObject background;
    Text signReadText = null, signText = null;
    bool signActive = false, inRadius = false;
    public string textOnSign = "";

    void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        signReadText = GameObject.Find("SignReadText").GetComponent<Text>();
        signText = GameObject.Find("SignText").GetComponent<Text>();
        background = GameObject.Find("PopUpBackground");

        signReadText.enabled = false;
        inRadius = false;
        signText.text = "";
        background.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || (Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            signActive = !signActive;
        }

        if (inRadius == false)
        {
            signActive = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactive")
        {
            signReadText.transform.position = cam.WorldToScreenPoint(transform.position + new Vector3(0, 2.8f, 0));
            signReadText.text = "Press F To Read Sign";
            signReadText.enabled = true;
            inRadius = true;

            if (signActive)
            {
                signText.text = textOnSign.ToString();
                background.GetComponent<Image>().color = Color.Lerp(background.GetComponent<Image>().color, new Color(0.75f, 0.75f, 0.75f, 0.5f), Time.deltaTime * 10);
            }
            else
            {
                signText.text = "";
                background.GetComponent<Image>().color = Color.Lerp(background.GetComponent<Image>().color, new Color(0.75f, 0.75f, 0.75f, 0), Time.deltaTime * 10);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactive")
        {
            signReadText.enabled = false;
            inRadius = false;
            signText.text = "";
            background.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 0);
        }
    }
}
