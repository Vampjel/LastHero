using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour, IPointerClickHandler
{

    public bool isAllPause;
    public bool isPlay;
    public bool isRestart;
    public bool isMainMenu;
    public bool isNextLevel;
    public Canvas UIcanvas;
    private static int clicks = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (WinLose.showWinStage)
        {
            if (isAllPause)
            {
                if (clicks >= 2)
                    clicks = 1;
                else
                    clicks = clicks + 1;

                if (clicks == 1)
                {
                    PlayerController.allPause = true;
                    UIcanvas.GetComponent<Canvas>().enabled = true;
                }
                else if (clicks == 2)
                {
                    PlayerController.allPause = false;
                    UIcanvas.GetComponent<Canvas>().enabled = false;
                }
            }
            else if (isPlay)
            {
                clicks = clicks + 1;
                PlayerController.allPause = false;
                UIcanvas.GetComponent<Canvas>().enabled = false;
            }
        }
        if (isRestart)
        {
            clicks = 0;
            PlayerController.isDeath = false;
            PlayerController.allPause = false;
            Application.LoadLevel(Application.loadedLevel);
        }
        else if (isNextLevel)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelComplite"));
        }
    }

    private void FixedUpdate()
    {
        if (PlayerController.isDeath && !WinLose.showWinStage)
        {
            WinLose.showWinStage = false;
            PlayerController.allPause = true;
            UIcanvas.GetComponent<Canvas>().enabled = false;
        }
    }
}