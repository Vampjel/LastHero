using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    //===========================//
    //      Базовые настройки    //
    //===========================//
    [Header("Базовые найстройки")]
    public float speed;
    public float max_health;
    public float damage;
    [Header("Настройки атаки")]
    public float agroDis;
    public float attackDistance;
    public float attackColownBase;
    [Header("Прочие настройки")]
    public bool isLeftFlip;
    public float patrol_left;
    public float patrol_right;
    public string path_effect;
    [Header("Для дальних монстров")]
    public bool isRanged;
    public string missible;
    public float speedMissible;
    public float distanceMissible;
    //===========================//
    //   Локальные переменные    //
    //===========================//
    private float attack_colown;
    private float attack_time_base;
    private float attack_time;
    private float hurt_base;
    private float hurt;
    private bool isAttack = false;
    private bool isVisible = false;
    //===========================//
    //    прочие переменные      //
    //===========================//
    private float death_timeBase;
    private float death_time;
    private float health;
    private bool isDeath;
    //===========================//
    private Transform player_Transform;
    private PlayerController player_Script;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 localScale;
    //===========================//
    //    Полоса жизни           //
    //===========================//
    private Image healthBar;
    private Image healthBar2;
    private float fadeTime_base = 255f;
    private float fadeTime;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player_Script = FindObjectOfType<PlayerController>();
        localScale = transform.localScale;

        player_Transform = player_Script.transform;
        UpdateAnimClipTimes();
        //====================================/
        health = max_health;
        healthBar = transform.GetChild(1).transform.GetChild(0).GetComponent<Image>(); // red
        healthBar2 = transform.GetChild(1).transform.GetChild(1).GetComponent<Image>();// black
        //====================================/
        if (speed < 0)
        {
            if (!isLeftFlip)
                localScale.x = Mathf.Abs(localScale.x) * (-1f);
            else
                localScale.x = Mathf.Abs(localScale.x);
        }
        else
        {
            if (isLeftFlip)
                localScale.x = Mathf.Abs(localScale.x) * (-1f);
            else
                localScale.x = Mathf.Abs(localScale.x);
        }

    }

    private void GetEffects()
    {
        if (transform.position.x > player_Script.transform.position.x)
        {
            Instantiate(Resources.Load("bloodEffect/" + path_effect) as GameObject, transform.position, Quaternion.Euler(0f, 90, 90f));
            Instantiate(Resources.Load("Effects/sfxSword") as GameObject, transform.position, Quaternion.Euler(0f, 90, 90f));
        }
        else
        {
            Instantiate(Resources.Load("bloodEffect/" + path_effect) as GameObject, transform.position, Quaternion.Euler(-180, 90, 90f));
            Instantiate(Resources.Load("Effects/sfxSword") as GameObject, transform.position, Quaternion.Euler(-180, 90, 90f));
        }
    }

    public void DamageTaken(float damage)
    {
        health = health - damage;
        hurt = hurt_base;
        fadeTime = fadeTime_base;
        if (health <= 0f)
        {
            if (!isDeath)
            {
                isDeath = true;
                death_time = death_timeBase;
            }
        }
        //===========================//
        //       healthBar           //
        //===========================//
        if (isDeath)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
        else
        {
            healthBar2.fillAmount = (health / max_health);
        }
        GetEffects();
    }

    private void AttackFunc()
    {
        GameObject fire;
        if (attack_time <= 0f)
        {
            isAttack = false;
            if (isRanged)
            {
                fire = Resources.Load("Missibles/" + missible) as GameObject;
                if (transform.position.x > player_Script.transform.position.x)
                    fire.GetComponent<MissiblesRanges>().setVariables(Mathf.Abs(speedMissible) * (-1f), distanceMissible, damage);
                else
                    fire.GetComponent<MissiblesRanges>().setVariables(Mathf.Abs(speedMissible), distanceMissible, damage);

                Instantiate(fire, transform.GetChild(0).position, Quaternion.identity);
            }
            else
            {
                bool damageTake = Physics2D.OverlapCircle(transform.GetChild(0).position, 1f, 1 << LayerMask.NameToLayer("Player"));
                if (damageTake)
                {
                    player_Script.TakeDamage(damage);
                }
            }
        }
        else
        {
            attack_time = attack_time - Time.deltaTime;
            anim.SetInteger("anim", 4);
        }
    }

    private void Flip()
    {
        Vector2 position = transform.position;
        Vector2 playerPos = player_Transform.position;
        if (position.x > playerPos.x)
        {
            if (!isLeftFlip)
                localScale.x = Mathf.Abs(localScale.x) * (-1f);
            else
                localScale.x = Mathf.Abs(localScale.x);
            speed = Mathf.Abs(speed) * (-1f);
        }
        else if (position.x < playerPos.x)
        {
            speed = Mathf.Abs(speed);
            if (!isLeftFlip)
                localScale.x = Mathf.Abs(localScale.x);
            else
                localScale.x = Mathf.Abs(localScale.x) * (-1f);
        }
        transform.localScale = localScale;
    }

    void Update()
    {
        float distance;
        float f = 0f;
        //===========================//
        //          death            //
        //===========================//
        if (!PlayerController.allPause && player_Script.gameObject != null)
        {
            if (isDeath)
            {
                if (death_time <= 0)
                {
                    player_Script.TotalMonsters();
                    Destroy(gameObject);
                }
                else
                {
                    death_time = death_time - Time.deltaTime;
                    anim.SetInteger("anim", 2);
                }
            }
            else
            {
                if (!PlayerController.isDeath && player_Script != null)
                {
                    //===========================//
                    //          attack           //
                    //===========================//
                    if (isAttack)
                        AttackFunc();

                    if (attack_colown > 0)
                        attack_colown = attack_colown - Time.deltaTime;
                    Vector2 posA = transform.position;
                    Vector2 posB = player_Script.transform.position;


                    if (isRanged)
                        f = Mathf.Abs(posA.y - posB.y);
                    distance = Vector2.Distance(posA, posB);
                    if (hurt > 0)
                    {
                        hurt = hurt - Time.deltaTime;
                        if (!isAttack)
                            anim.SetInteger("anim", 3);
                    }
                    else
                    {

                        bool ff;
                        if (isRanged)
                            ff = distance <= attackDistance && f < 1f;
                        else
                            ff = distance <= attackDistance;
                        if (ff)
                        {
                            if (attack_colown <= 0f)
                            {
                                isAttack = true;
                                attack_time = attack_time_base;
                                attack_colown = attackColownBase;
                            }
                            if (!isAttack)
                            {
                                anim.SetInteger("anim", 0);
                            }
                            Flip();
                        }
                        else
                        {
                            if (speed != 0f)
                            {
                                if (isVisible)
                                {
                                    Flip();
                                    isVisible = false;
                                }
                                anim.SetInteger("anim", 1);
                                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                            }
                            else if (speed == 0f)
                            {
                                anim.SetInteger("anim", 0);
                            }

                        }
                    }
                }
            }
        }
    }



    private void Patrol()
    {
        if (patrol_left > transform.position.x)
        {
            speed = Mathf.Abs(speed);
            if (isLeftFlip)
                localScale.x = Mathf.Abs(localScale.x) * (-1f);
            else
                localScale.x = Mathf.Abs(localScale.x);
        }
        else if (patrol_right < transform.position.x)
        {
            speed = Mathf.Abs(speed) * (-1f);
            if (isLeftFlip)
                localScale.x = Mathf.Abs(localScale.x);
            else
                localScale.x = Mathf.Abs(localScale.x) * (-1f);
        }
        transform.localScale = localScale;

    }

    private void FixedUpdate()
    {
        if (!PlayerController.allPause && player_Script.gameObject != null)
        {
            Vector3 pos = transform.position + Vector3.right * agroDis;
            RaycastHit2D hit = Physics2D.Linecast(transform.position + Vector3.left * agroDis, pos, 1 << LayerMask.NameToLayer("Player"));

            if (hit.collider != null)
            {
                isVisible = true;
            }
            else
            {
                Patrol();
            }
            Debug.DrawLine(transform.position + Vector3.left * agroDis, pos, Color.blue);
            //===========================//
            //       healthBar           //
            //===========================//
            if (fadeTime <= 0f || isDeath)
            {
                healthBar.color = new Color32(0, 0, 0, 0);
                healthBar2.color = new Color32(255, 0, 0, 0);
            }
            else
            {
                fadeTime = fadeTime - 3f;
                healthBar.color = new Color32(0, 0, 0, (byte)fadeTime);
                healthBar2.color = new Color32(255, 0, 0, (byte)fadeTime);
            }
        }
    }
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "hurt":
                    hurt_base = clip.length;
                    break;

                case "attack":
                    attack_time_base = clip.length;
                    break;
                case "death":
                    death_timeBase = clip.length;
                    break;
            }
        }
    }
}
    
