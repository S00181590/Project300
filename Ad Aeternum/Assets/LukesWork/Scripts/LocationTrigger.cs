using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationTrigger : MonoBehaviour
{
    Image locationImage;
    public GameObject image;
    bool imageBool;
    Canvas canvas;

    void Start()
    {
        canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        image.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        imageBool = false;
    }

    void Update()
    {
        if (locationImage != null)
        {
            if (imageBool == true)
            {
                //locationImage.CrossFadeAlpha(1, 3, false);
                locationImage.color = Color.Lerp(locationImage.color, new Color(1, 1, 1, 1), Time.deltaTime * 2);
            }
            else
            {
                //locationImage.CrossFadeAlpha(1, 3, false);
                locationImage.color = Color.Lerp(locationImage.color, new Color(1, 1, 1, 0), Time.deltaTime * 2);
            }

            if (locationImage.color == new Color(1, 1, 1, 0))
            {
                Destroy(locationImage.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.Find("Player"))
        {
            if (locationImage == null)
                StartCoroutine(ImageDelete(locationImage));
        }
    }

    public IEnumerator ImageDelete(Image locImage)
    {
        locationImage = Instantiate(image.GetComponent<Image>(), new Vector3(Screen.width / 2, 375, 0), Quaternion.identity, canvas.transform);
        locationImage.color = new Color(1, 1, 1, 0);

        imageBool = true;

        yield return new WaitForSecondsRealtime(5);

        imageBool = false;
    }
}
