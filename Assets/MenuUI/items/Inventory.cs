using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    public static int itemId;
    public int itemID;
    public bool isShop;
    public bool isItems;
    private int[] price = new int[12] { 250, 500, 1500, 5000, 250, 500, 1500, 5000, 250, 500, 1500, 5000 };

    private string[] info = new string[12] {" Simple Sword \n\n Gives\n + 2 damage"," Warrior sword \n\n Gives\n + 5 damage",
    " Demonic Sword \n\n Gives\n + 10 damage", " Paladin Sword \n\n Gives \n+35 damage", "cap \n\n Gives\n + 5 Health","iron helmet \n\n Gives\n + 10 Health",
     "warrior helmet \n\n Gives\n + 20 Health",
     "sun helmet \n\n Gives\n + 50 Health",
     "simple ring \n\n Gives\n + 4 Health",
     "magic ring \n\n Gives\n + 8 Health",
     "king's ring \n\n Gives\n + 18 Health",
     "demonic ring \n\n Gives\n + 45 Health" };
    private float[] damages   = new float[4] { 2f, 5f, 10f, 35f }; // swords
    private float[] hitPoints = new float[4] { 5f, 10f, 20f, 50f };
    private float[] rings     = new float[4] { 4f, 8f, 18f, 45f };
    private string[] items    = new string[12] { "sword1", "sword2", "sword3", "sword4", "helmet1", "helmet2", "helmet3", "helmet4", "ring1", "ring2", "ring3", "ring4" };
    private void Start()
    {
        int gold;
        PlayerPrefs.GetFloat("damage", 0);
        PlayerPrefs.GetFloat("helmet", 0);
        PlayerPrefs.GetFloat("rings", 0);
        if (isShop)
        {
            gold = PlayerPrefs.GetInt("coin");
            for (int i = 0; i < items.Length; i++)
            {
                if(PlayerPrefs.GetString(items[i]) == "Equiped")
                {
                    transform.parent.transform.GetChild(1).transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.yellow;
                    transform.parent.transform.GetChild(1).transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Equiped";
                }

            }
            Debug.Log(PlayerPrefs.GetFloat("damage"));
            Debug.Log(PlayerPrefs.GetFloat("helmet"));
            Debug.Log(PlayerPrefs.GetFloat("rings"));
            transform.parent.transform.GetChild(5).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gold.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int i = PlayerPrefs.GetInt("coin");
        string[] p = new string[12];
        float i_damage = 0f, i_hitpoints = 0f, i_rings = 0f;
        if (isShop)
        {
            if (i >= price[itemId] && transform.parent.GetChild(1).GetChild(itemId).GetChild(2).GetComponent<TextMeshProUGUI>().text != "Equiped")
            {
                transform.parent.GetChild(1).GetChild(itemId).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Equiped";
                transform.parent.GetChild(1).GetChild(itemId).GetChild(0).GetComponent<Image>().color = Color.yellow;
                PlayerPrefs.SetInt("coin", i - price[itemId]);
                i = PlayerPrefs.GetInt("coin");
                transform.parent.GetChild(4).GetComponent<TextMeshProUGUI>().text = i.ToString();
                for(int ii = 0; ii < transform.parent.transform.GetChild(1).childCount; ii++)
                {
                    p[ii] = transform.parent.transform.GetChild(1).transform.GetChild(ii).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;
                    if (p[ii] == "Equiped")
                    {
                        if (ii >= 8)
                        {
                            i_rings = i_rings + rings[ii-8];
                            Debug.Log(i_rings + " rings");
                            PlayerPrefs.SetString(items[ii], "Equiped");
                            PlayerPrefs.SetFloat("rings", i_rings);
                        }
                        else if (ii >= 4)
                        {
                            i_hitpoints = i_hitpoints + hitPoints[ii-4];
                            Debug.Log(i_hitpoints + " helmet");
                            PlayerPrefs.SetString(items[ii], "Equiped");
                            PlayerPrefs.SetFloat("helmet", i_hitpoints);
                        }
                        else
                        {
                            i_damage = i_damage + damages[ii];
                            Debug.Log(i_damage + " damage");
                            PlayerPrefs.SetString(items[ii], "Equiped");
                            PlayerPrefs.SetFloat("damage", i_damage);
                        }
                        transform.parent.transform.GetChild(5).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("coin").ToString();
                        PlayerPrefs.Save();
                    }
                }
            }
            else
            {
                Debug.Log("Malo Deneg");
            }
        }
        else if (isItems)
        {
            itemId = itemID;
            transform.parent.transform.parent.GetChild(4).GetComponent<TextMeshProUGUI>().text = info[itemId];
        }
        else
        {
            Debug.Log("Error 404");
        } 
    }
}
