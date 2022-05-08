using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;
using TMPro;
public class ads : MonoBehaviour, IPointerClickHandler
{
    public string id;
    void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(id,false);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 100);
        }
    }
}
