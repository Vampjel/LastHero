using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jump : MonoBehaviour, IPointerClickHandler
{
    PlayerController player_Script;

    public void OnPointerClick(PointerEventData eventData)
    {
        player_Script.JumpMobile();
    }

    void Start()
    {
        player_Script = FindObjectOfType<PlayerController>();
    }


}
