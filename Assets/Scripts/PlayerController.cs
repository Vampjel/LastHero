using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    [Header("Базовые настройки")]
    private float  baseHealth;
    private float  base_Damage;
    [Header("Прочие настройки")]
    public static float speed;
    public LayerMask bossMask;
    private float health;
    //=====================//
    //        Атака        //
    //=====================//
    private float attack_time_base;
    private float attack_time = 0f;
    private float comboTimer  = 0f;
    private int   comboIndex  = 2;
    private bool  isAttacking = false;
    private bool  isAlive = true;
    //=====================//
    //         jump        //
    //=====================//
    private bool groundCheck;
    private bool groundEffect;
    public  Rigidbody2D rb;
    private Animator    animator;
    private SpriteRenderer render;
    private CameraShake camShake;

    private GameObject win_stage_object;
    private Vector2    isWinStagePosition;
    private WinLose    LoseScript;
    //=====================//
    //     Полоса жизни    //
    //=====================//
    private Image healthBar;
    private Image healthBar2;
    private float fadeTime_base = 255f;
    private float fadeTime;
    //=====================//
    //  Прочие переменные  //
    //=====================//
    private float  death_time_base;
    private float  death_time;
    public  static bool  isDeath  = false;
    public  static bool  allPause = false;
    //====================//
    public int     totalMonsters = 0;
    //====================//
    public TextMeshProUGUI GoldText;
    public static int gold = 0;
    void Start()
    {
        allPause = false;
        isDeath  = false;
        //=========================
        LoseScript = GetComponent<WinLose>();
        win_stage_object = GameObject.FindGameObjectWithTag("Win");
        isWinStagePosition = win_stage_object.transform.position;
        Destroy(win_stage_object);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        camShake = FindObjectOfType<CameraShake>();
        render = GetComponent<SpriteRenderer>();
        UpdateAnimClipTimes();
        //=========================
        baseHealth  = 8;
        base_Damage = 3;
        //=========================
        baseHealth = baseHealth + (PlayerPrefs.GetFloat("Helmet") + PlayerPrefs.GetFloat("rings"));
        health     = baseHealth;
        healthBar  = transform.GetChild(2).transform.GetChild(1).GetComponent<Image>(); // red
        healthBar2 = transform.GetChild(2).transform.GetChild(0).GetComponent<Image>(); // black
        //=========================
        base_Damage = base_Damage + (PlayerPrefs.GetFloat("damage"));
        Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.position, 30000, 1 << LayerMask.NameToLayer("Monsters"));
        for (int i = 0; i < enemy.Length; i++)
        {
            totalMonsters = totalMonsters + 1;
        }
        //==========================
        //       coin function
        gold = PlayerPrefs.GetInt("coin", 0);
        GoldText.text = gold.ToString();
    }

    //===========================//
    //       Coin Add            //
    //===========================//


    public void coin_add()
    {
        int coin = PlayerPrefs.GetInt("coin");
        PlayerPrefs.SetInt("coin", coin + 1);
        GoldText.text = coin.ToString();
        PlayerPrefs.Save();
    }


    public void TotalMonsters()
    {
        totalMonsters = totalMonsters - 1;
    }
    
    public void JumpR(float impulse)
    {
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, impulse), ForceMode2D.Impulse);
    }

    private void GetKeys()
    {
        if ((Input.GetKeyDown(KeyCode.R) || Attack.isAttack) && attack_time <= 0f)
        {
            isAttacking = true;
            attack_time = attack_time_base;
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundCheck)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, 850f));
            Sounds.PlaySound("JumpN");
        }
    }
 

    public void JumpMobile()
    {
        if (groundCheck)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, 850f));
            Sounds.PlaySound("JumpN");
        }
    }

    private void MoveHero( bool isGrounded) {
        attack_time = 0f;
       
        if (Input.GetAxis("Horizontal") != 0)
        {
            speed = 6f;
            Sounds.PlaySound("run");
            if (isGrounded)
                animator.SetInteger("anim", 1);
        }
        else
        {
            speed = 0f;
            if (isGrounded)
                animator.SetInteger("anim", 0);
        }

        if (!isGrounded) groundEffect = true;
        if (groundCheck && groundEffect)
        {
            groundEffect = false;
            Sounds.PlaySound("jump");
            Instantiate(Resources.Load("Effects/dustsfxParticle"), new Vector2(transform.GetChild(0).position.x, transform.GetChild(0).position.y - 0.25f), Quaternion.identity);
        }
        
        if (Input.GetAxis("Horizontal") < 0)
            transform.localScale = new Vector3(-3, 3, 3);
        else if (Input.GetAxis("Horizontal") > 0)
            transform.localScale = new Vector3(3, 3, 3);
        transform.position = new Vector2(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, transform.position.y);
    }

    private void MoveHeroMobile(bool isGrounded)
    {
        attack_time = 0f;

        if (speed != 0)
        {
            Sounds.PlaySound("run");
            if (isGrounded)
                animator.SetInteger("anim", 1);
        }
        else
        {
            if (isGrounded)
                animator.SetInteger("anim", 0);
        }

        if (!isGrounded) groundEffect = true;
        if (groundCheck && groundEffect)
        {
            groundEffect = false;
            Sounds.PlaySound("jump");
            Instantiate(Resources.Load("Effects/dustsfxParticle"), new Vector2(transform.GetChild(0).position.x, transform.GetChild(0).position.y - 0.25f), Quaternion.identity);
        }

        if (speed < 0)
            transform.localScale = new Vector3(-3, 3, 3);
        else if (speed > 0)
            transform.localScale = new Vector3(3, 3, 3);
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }

    public void TakeDamage(float takeDamage)
    {
        health = health - takeDamage;
        if (health <= 0f)
        {
            isDeath    = true;
            death_time = death_time_base;
        }
        else
        {
            healthBar.fillAmount = (health / baseHealth);
            fadeTime = fadeTime_base;
        }
        Instantiate(Resources.Load("bloodEffect/PlayerBlood") as GameObject, transform.position, Quaternion.identity);
        Shake(0.12f, 0.16f);
        Debug.Log("damageTaked");
    }

    public void Shake(float shakeDuration, float shakeAmount)
    {
        camShake.shakeDuration  = shakeDuration;
        camShake.shakeAmount    = shakeAmount;
        camShake.decreaseFactor = 1f;
    }
    private void getEffects()
    {
        Shake(0.22f, 0.20f);
        Instantiate(Resources.Load("Effects/slashEffect") as GameObject, transform.GetChild(1).position, Quaternion.identity);
    }

    private bool isCheker()
    {
        if (isDeath)
            return true;
        else
            return false;
    }

    void Update()
    {
        GetKeys();
        //===========================//
        //          death            //
        //===========================//
        if (!allPause || isDeath )
        {
            if (totalMonsters == 0 && !isDeath && totalMonsters != -1)
            {
                totalMonsters = -1;
                Instantiate(Resources.Load("Win") as GameObject, isWinStagePosition, Quaternion.identity);
            }
            else { 
                if (isDeath && isCheker())
                {
                    if (death_time <= 0 && isAlive)
                    {
                        allPause = true;
                        isAlive = false;
                        LoseScript.Lose();
                        render.enabled = false;
                    }
                    else
                    {
                        death_time = death_time - Time.deltaTime;
                        animator.SetInteger("anim", 7);
                    }
                }
                else
                {
                    if (comboIndex == 6)
                        comboIndex = 2;

                    if (comboTimer > 0f)
                    {
                        comboTimer = comboTimer - Time.deltaTime;
                        if (comboTimer <= 0f)
                            comboIndex = 2;
                    }
                    //===========================//
                    //          attack           //
                    //===========================//
                    if (isAttacking)
                    {
                        if (attack_time <= 0f)
                        {
                            isAttacking = false;
                            Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.GetChild(1).position, 0.6f, 1 << LayerMask.NameToLayer("Monsters"));
                            if (comboIndex == 2) Sounds.PlaySound("slash1");
                            else if (comboIndex == 3) Sounds.PlaySound("slash2");
                            else if (comboIndex == 4) Sounds.PlaySound("slash3");
                            else if (comboIndex == 5) Sounds.PlaySound("slash4");
                            for (int i = 0; i < enemy.Length; i++)
                            {
                                comboTimer = 2.05f;
                                comboIndex = comboIndex + 1;
                                enemy[i].GetComponent<EnemyScript>().DamageTaken(base_Damage);
                                getEffects();
                                break;
                            }
                            Collider2D box = Physics2D.OverlapCircle(transform.GetChild(1).position, 0.6f, 1 << LayerMask.NameToLayer("boxes"));
                            if(box != null) box.GetComponent<boxScript>().damageTakeFunc(1f);

                            Collider2D boss = Physics2D.OverlapCircle(transform.GetChild(1).position, 0.6f, bossMask);
                            if (boss != null) boss.GetComponent<BossScript>().DamageTake(base_Damage);
                        }
                        else
                        {
                            attack_time = attack_time - Time.deltaTime;
                            animator.SetInteger("anim", comboIndex);
                        }
                    }
                    else
                    {
                        bool monsterCheck = Physics2D.OverlapCircle(transform.GetChild(0).position, 0.25f, 1 << LayerMask.NameToLayer("Monsters"));
                        groundCheck = Physics2D.OverlapCircle(transform.GetChild(0).position, 0.3f, 1 << LayerMask.NameToLayer("ground"));
                        if (!groundCheck)
                        {
                            if (monsterCheck)
                                animator.SetInteger("anim", 0);
                            else
                                animator.SetInteger("anim", 6);
                        }
                        //MoveHero(groundCheck); PC
                        MoveHeroMobile(groundCheck); // Mobile
                    }
                }
            }
        }
    }

    public void onSetDeathTime()
    {
        death_time = death_time_base;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DeathZone")
        {
            isDeath = true;
            onSetDeathTime();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "gold")
        {
            Sounds.PlaySound("coin");
            coin_add();
            Instantiate(Resources.Load("Effects/coinEffect") as GameObject, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }else if(collision.gameObject.tag == "DeathZone")
        {
            TakeDamage(1000f);
        }
    }
    private void FixedUpdate()
    {
        //===========================//
        //       healthBar           //
        //===========================//
        if (fadeTime <= 0f)
        {
            healthBar.color = new Color32(255, 0, 0, 0);
            healthBar2.color = new Color32(0, 0, 0, 0);
        }
        else
        {
            fadeTime = fadeTime - 3f;
            healthBar.color = new Color32(255, 0, 0, (byte)fadeTime);
            healthBar2.color = new Color32(0, 0, 0, (byte)fadeTime);
        }
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "attack_1":
                    attack_time_base = clip.length;
                    break;
                case "death":
                    death_time_base = clip.length;
                    break;
            }
        }
    }
}
