using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpIconSwitch : MonoBehaviour
{
    public Image iconImage;
    public Image glowImage;

    List<Color> colourList = new List<Color>()
    {
        new Color(1, 0.18f, 0.81f),
        new Color(1, 0.55f, 0.18f),
        new Color(0, 0.5f, 0.05f)
    };

    public int i = 0;

    bool numBool = true;
    float n;

    bool active = false;

    void Update()
    {
        if (active == false)
        {
            iconImage.color = new Color(0.2f, 0.2f, 0.2f);
            glowImage.color = new Color(0, 0, 0);

            Invoke("Activate", 5);
        }
        else
        {
            iconImage.color = colourList[i];
            glowImage.color = colourList[i];

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (i < colourList.Count - 1)
                    i++;
                else
                    i = 0;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (i > 0)
                    i--;
                else
                    i = colourList.Count - 1;
            }

            iconImage.transform.localScale = Vector3.Lerp(iconImage.transform.localScale, new Vector3(n, n, n), 20 * Time.deltaTime);
            glowImage.transform.localScale = Vector3.Lerp(glowImage.transform.localScale, new Vector3(n * 3, n * 3, n * 3), 5 * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                numBool = !numBool;
                InvokeRepeating("Resize", 0.2f, 100000);
            }

            if (numBool == true)
            {
                n = 0.5f;
            }
            else
            {
                n = 0.75f;
            }

            iconImage.transform.Rotate(new Vector3(0, 0, 2));
        }
    }

    void Resize()
    {
        numBool = !numBool;
        CancelInvoke();
    }

    void Activate()
    {
        active = true;
        CancelInvoke();
    }
}
