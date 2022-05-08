using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour, IPointerClickHandler
{
    public bool isMainMenu;
    public bool isPlay;
    public bool isOptions;
    public bool isExit;
    public Canvas options,MainMenu;
    public bool isShop;

    public static float effectVoice;
    public static float musicVoice;

    private Slider s1, s2;

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        if (isMainMenu)
        {
            effectVoice = PlayerPrefs.GetFloat("effectVoice", 0.90f);
            musicVoice  = PlayerPrefs.GetFloat("musicVoice", 0.70f);
            s1 = options.transform.GetChild(1).GetComponent<Slider>();
            s2 = options.transform.GetChild(3).GetComponent<Slider>();
            s1.value = effectVoice;
            s2.value = musicVoice;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMainMenu)
        {
            options.GetComponent<Canvas>().enabled = false;
            MainMenu.GetComponent<Canvas>().enabled = true;
            PlayerPrefs.SetFloat("effectVoice", s1.value);
            PlayerPrefs.SetFloat("musicVoice", s2.value);
            PlayerPrefs.Save();
        }
        else if (isPlay)
        {
            Application.LoadLevel(1);
        }
        else if (isOptions)
        {
            if (options.GetComponent<Canvas>().enabled)
            {
                options.GetComponent<Canvas>().enabled = false;
                MainMenu.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                options.GetComponent<Canvas>().enabled = true;
                MainMenu.GetComponent<Canvas>().enabled = false;
            }
        }
        else if (isShop)
        {
            Application.LoadLevel(10);
        }
        else if (isExit)
        {
            Application.Quit();
        }
    }
}

