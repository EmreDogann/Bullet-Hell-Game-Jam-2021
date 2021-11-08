using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCursor : MonoBehaviour
{
    public GameObject cursor;
    public float cursorPosX;
    public float cursorPosY;

    private Camera camera;

    void Update()
    {
        cursor.SetActive(true);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.nearClipPlane;
        Vector2 worldPosition = camera.ScreenToWorldPoint(mousePos);
        transform.position = worldPosition;
    }
}
