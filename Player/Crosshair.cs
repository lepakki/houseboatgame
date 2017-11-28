using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    public Texture2D crosshairImage;
    public Rect position;
    private Vector3 mouse_pos;

    void Start()
    {

    }

    void Update()
    {
        mouse_pos = Input.mousePosition;
    }

    void OnGUI()
    {
        float xMin = Screen.width - (Screen.width - mouse_pos.x) - (crosshairImage.width / 2);
        float yMin = (Screen.height - mouse_pos.y) - (crosshairImage.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }
}