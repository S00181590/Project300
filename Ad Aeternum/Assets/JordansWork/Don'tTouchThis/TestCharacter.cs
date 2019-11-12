using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{

    float Horizontal, Vertical, Speed, startY;

    // Start is called before the first frame update
    void Start()
    {

        Speed = 1f;
        startY = transform.position.y;

        Camera.main.transform.position = gameObject.transform.position;

        Camera.main.transform.SetParent(gameObject.transform);
        Camera.main.transform.localPosition = new Vector3(0, 1, -2);
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal")*Speed * Time.deltaTime;
        Vertical = Input.GetAxis("Vertical")*Speed;
        float MouseRotationX = Input.GetAxis("Mouse X");
        if(MouseRotationX < 0 || MouseRotationX > 0)
        {
            transform.Rotate(new Vector3(0, MouseRotationX, 0));
        }
        transform.Translate(new Vector3(Horizontal, 0, Vertical));


    }
}
