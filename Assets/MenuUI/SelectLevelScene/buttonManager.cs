using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour, IPointerClickHandler
{
    public int lockLevel = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(lockLevel);
    }
}
