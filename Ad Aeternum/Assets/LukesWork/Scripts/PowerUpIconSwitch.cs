using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpIconSwitch : MonoBehaviour
{
    Image iconImage, glowImage, swordIcon, shieldIcon, speedIcon;
    AudioSource iconBlipSFX, poweredUpSFX;
    float rotateSpeed = 40, n;
    int i = 0;
    bool dpadActive = false, active = true, numBool = true;
    public bool attack = false, defence = false, speed = false;
    StateManager state;

    public List<Color> colourList = new List<Color>() { };

    private void Start()
    {
        iconImage = GameObject.Find("PowerUpImage").GetComponent<Image>();
        glowImage = GameObject.Find("PowerUpGlow").GetComponent<Image>();

        swordIcon = GameObject.Find("SwordIcon").GetComponent<Image>();
        shieldIcon = GameObject.Find("ShieldIcon").GetComponent<Image>();
        speedIcon = GameObject.Find("SpeedIcon").GetComponent<Image>();

        iconBlipSFX = GameObject.Find("SFX_Blip").GetComponent<AudioSource>();
        poweredUpSFX = GameObject.Find("SFX_PoweredUp").GetComponent<AudioSource>();

        state = GameObject.Find("PlayerMoveController").GetComponent<StateManager>();
    }

    void Update()
    {
        if (attack)
        {
            
        }
        else
        {
            
        }

        if (defence)
        {

        }
        else
        {

        }

        if (speed)
        {
            state.speedMult = 1.75f;
        }
        else
        {
            state.speedMult = 1;
        }

        if (active == false)
        {
            iconImage.color = Color.Lerp(iconImage.color, new Color(0.2f, 0.2f, 0.2f), Time.deltaTime);
            glowImage.color = Color.Lerp(glowImage.color, new Color(0, 0, 0), Time.deltaTime);

            rotateSpeed = Mathf.Lerp(rotateSpeed, 10, Time.deltaTime * 1);

            Invoke("Activate", 10);
        }
        else
        {
            iconImage.color = Color.Lerp(iconImage.color, colourList[i], Time.deltaTime * 5);
            glowImage.color = Color.Lerp(glowImage.color, colourList[i], Time.deltaTime * 5);

            rotateSpeed = Mathf.Lerp(rotateSpeed, 250, Time.deltaTime * 1);

            if (Input.GetAxisRaw("Horizontal DPad") == 0f)
            {
                dpadActive = false;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (i < colourList.Count - 1)
                    i++;
                else
                    i = 0;

                iconBlipSFX.Play();
            }
            else if (Input.GetAxisRaw("Horizontal DPad") > 0f)
            {
                if (dpadActive == false)
                {
                    dpadActive = true;

                    if (i < colourList.Count - 1)
                        i++;
                    else
                        i = 0;

                    iconBlipSFX.Play();
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (i > 0)
                    i--;
                else
                    i = colourList.Count - 1;

                iconBlipSFX.Play();
            }
            else if (Input.GetAxisRaw("Horizontal DPad") < 0f)
            {
                if (dpadActive == false)
                {
                    dpadActive = true;

                    if (i > 0)
                        i--;
                    else
                        i = colourList.Count - 1;

                    iconBlipSFX.Play();
                }
            }

            iconImage.transform.localScale = Vector3.Lerp(iconImage.transform.localScale, new Vector3(n * 1.5f, n * 1.5f, n * 1.5f), 20 * Time.deltaTime);
            glowImage.transform.localScale = Vector3.Lerp(glowImage.transform.localScale, new Vector3(n * 3, n * 3, n * 3), 5 * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                numBool = !numBool;
                InvokeRepeating("Resize", 0.2f, 10);

                poweredUpSFX.Play();
            }

            if (numBool == true)
            {
                n = Mathf.Lerp(n, 0.3f, Time.deltaTime);
            }
            else
            {
                n = Mathf.Lerp(n, 0.5f, Time.deltaTime);
            }

            iconImage.transform.localScale = Vector3.Lerp(iconImage.transform.localScale,
                new Vector3(Mathf.PingPong(Time.time, 0.75f), Mathf.PingPong(Time.time, 0.75f), Mathf.PingPong(Time.time, 0.75f)), Time.deltaTime * 5);
        }

        if (i == 0)
        {
            swordIcon.enabled = true;
            shieldIcon.enabled = false;
            speedIcon.enabled = false;
        }
        else if (i == 1)
        {
            swordIcon.enabled = false;
            shieldIcon.enabled = true;
            speedIcon.enabled = false;
        }
        else if (i == 2)
        {
            swordIcon.enabled = false;
            shieldIcon.enabled = false;
            speedIcon.enabled = true;

        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            StartCoroutine(Buffs());
        }

        iconImage.transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }

    IEnumerator Buffs()
    {
        if (i == 0)
        {
            attack = true;

            yield return new WaitForSeconds(5);

            attack = false;
        }
        else if (i == 1)
        {
            defence = true;

            yield return new WaitForSeconds(5);

            defence = false;
        }
        else if (i == 2)
        {
            speed = true;

            yield return new WaitForSeconds(5);

            speed = false;
        }
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
