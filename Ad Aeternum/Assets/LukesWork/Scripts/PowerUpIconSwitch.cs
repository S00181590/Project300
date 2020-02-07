using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpIconSwitch : MonoBehaviour
{
    public Image iconImage;
    public Image glowImage;
    public AudioSource iconBlipSFX;
    public AudioSource poweredUpSFX;
    float rotateSpeed = 50;
    public int colourListAmount;

    public List<Color> colourList = new List<Color>()
    {
        //new Color(1, 0.18f, 0.81f),
        //new Color(1, 0.55f, 0.18f),
        //new Color(0, 0.5f, 0.05f)
    };

    public int i = 0;

    bool numBool = true;
    float n;

    bool active = true;

    void Update()
    {
        if (active == false)
        {
            iconImage.color = Color.Lerp(iconImage.color, new Color(0.2f, 0.2f, 0.2f), Time.deltaTime);
            glowImage.color = Color.Lerp(glowImage.color, new Color(0, 0, 0), Time.deltaTime);

            rotateSpeed = Mathf.Lerp(rotateSpeed, 10, Time.deltaTime * 1);

            Invoke("Activate", 5);
        }
        else
        {
            iconImage.color = Color.Lerp(iconImage.color, colourList[i], Time.deltaTime * 5);
            glowImage.color = Color.Lerp(glowImage.color, colourList[i], Time.deltaTime * 5);

            rotateSpeed = Mathf.Lerp(rotateSpeed, 250, Time.deltaTime * 1);

            if (Input.GetAxis("Mouse ScrollWheel") > 0f/* || (Input.GetAxis("Horizontal DPad") > 0f && (Input.GetAxis("Horizontal DPad") < 1f))*/)
            {
                if (i < colourList.Count - 1)
                    i++;
                else
                    i = 0;

                iconBlipSFX.Play();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f/* || (Input.GetAxis("Horizontal DPad") < 0f && (Input.GetAxis("Horizontal DPad") > -1f))*/)
            {
                if (i > 0)
                    i--;
                else
                    i = colourList.Count - 1;

                iconBlipSFX.Play();
            }

            iconImage.transform.localScale = Vector3.Lerp(iconImage.transform.localScale, new Vector3(n, n, n), 20 * Time.deltaTime);
            glowImage.transform.localScale = Vector3.Lerp(glowImage.transform.localScale, new Vector3(n * 3, n * 3, n * 3), 5 * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                numBool = !numBool;
                InvokeRepeating("Resize", 0.2f, 10);
                
                poweredUpSFX.Play();
            }

            if (numBool == true)
            {
                n = Mathf.Lerp(n, 0.25f, Time.deltaTime);
            }
            else
            {
                n = Mathf.Lerp(n, 0.5f, Time.deltaTime); ;
            }

            iconImage.transform.localScale = Vector3.Lerp(iconImage.transform.localScale, 
                new Vector3(Mathf.PingPong(Time.time, 1f), Mathf.PingPong(Time.time, 1f), Mathf.PingPong(Time.time, 1f)), Time.deltaTime * 5);
        }

        iconImage.transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }

    void Resize()
    {
        numBool = !numBool;
        rotateSpeed = Mathf.Lerp(rotateSpeed, 2000, Time.deltaTime * 100);
        InvokeRepeating("Deactivate", 0.3f, 10);
    }

    void Activate()
    {
        active = true;
        CancelInvoke();
    }

    void Deactivate()
    {
        active = false;
        CancelInvoke();
    }
}
