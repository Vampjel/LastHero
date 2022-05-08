using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Attack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isAttack;

    public void OnPointerDown(PointerEventData eventData)
    {
        isAttack = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        isAttack = false;
    }
}
