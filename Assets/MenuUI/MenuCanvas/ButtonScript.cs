using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public bool isLeftBtn;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (isLeftBtn)
            PlayerController.speed = -6f;
        else
            PlayerController.speed = 6f;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        PlayerController.speed = 0;
    }
}
