using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnaliticsControll : MonoBehaviour
{
    public void OnPlayerDead(int levelId, string characterName)
    {
        Analytics.CustomEvent(customEventName: "Player dead", eventData: new Dictionary<string, object>()
        {
            {"Level", levelId },
            {"Character", characterName }
        });
    }

    public void OnLevelStarted(int levelId, string characterName)
    {
        Analytics.CustomEvent(customEventName: "Level Started", eventData: new Dictionary<string, object>()
        {
            {"Level", levelId },
            {"Character", characterName }
        });
    }