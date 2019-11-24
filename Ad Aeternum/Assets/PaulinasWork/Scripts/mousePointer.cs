using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePointer : MonoBehaviour
{
    public Texture2D cursorImage;

    private int cursorWidth = 100;
    private int cursorHeight = 100;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), cursorImage);
    }
}
