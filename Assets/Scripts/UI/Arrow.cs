using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Arrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cursor;
    public GameObject button;

    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        cursor.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursor.SetActive(true);
        cursor.transform.position = new Vector3(button.transform.position.x - dist, button.transform.position.y);
        // print("OVER");
        //  = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursor.SetActive(false);
        // print("EXIT");
        // MenuCursor.free = true;
    }
}
