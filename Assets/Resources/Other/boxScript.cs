using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    public  float baseHealth;
    public  int   coins;
    private float damageTake;
    private SpriteRenderer render;
    private bool isActive = false;
    private PlayerController player_Script;
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        player_Script = FindObjectOfType<PlayerController>();
    }

    public void damageTakeFunc(float damage)
    {
        baseHealth = baseHealth - damage;
        damageTake = 0.25f;
        if(baseHealth <= 0)
        {
            Sounds.PlaySound("box");
            GetComponent<CreateCoins>().CreateCoinss(coins);
            Instantiate(Resources.Load("Effects/brush") as GameObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Instantiate(Resources.Load("Effects/slashEffect") as GameObject, transform.position, Quaternion.identity);
        player_Script.Shake(0.20f, 0.20f);
    }

    void Update()
    {
        if(damageTake <= 0f)
        {
            if (!isActive)
            {
                isActive = true;
                render.color = new Color32(255, 255, 255, 255);
            }
        }
        else
        {
            damageTake = damageTake - Time.deltaTime;
            if (isActive)
            {
                isActive = false;
                render.color = new Color32(255, 50, 50, 125);
            }
        }
    }
}
