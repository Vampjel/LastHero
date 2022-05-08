using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    [Header("основное")]
    public float base_health;
    [Header("атака и движение")]
    public float attack_damage;
    public float attack_cooldown;
    public float attack_distance;
    public float movement_speed; 
    [Header("Прочие настройки")]
    public bool  isLeftFlip;
    public float agroDistance;
    //===========================//
    protected float health;
    protected float attack_cd;
    protected float attack_duration_base;
    protected float attack_duration;
    protected float death_time_base;
    protected float hurt_time_base;
    protected float move_time_base;
    protected bool  is_hurt = true;
    protected bool  is_attack = false;
    protected bool  is_move = false;
    //===========================//
    protected Vector3  localScale;
    protected Vector2  start_position;
    protected Animator anim;
    protected PlayerController player_Script;
    protected Image healthBar;
    protected enum State { IDLE,WALK,DEATH,HURT,ATTACK };
    protected State state = State.ATTACK;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player_Script = FindObjectOfType<PlayerController>();
        player_Script.totalMonsters = + 1 ;

        UpdateAnimClipTimes();

        attack_duration = attack_duration_base;
        health          = base_health;
        localScale      = transform.localScale;
        start_position  = transform.position;
        healthBar = transform.GetChild(1).transform.GetChild(1).GetComponent<Image>();
    }


    public void DamageTake(float damage)
    {
        health = health - damage;
        healthBar.fillAmount = health / base_health;
        if (health <= 0f)
            state = State.DEATH;
        else
            state = State.HURT;
    }
    protected void LateUpdate()
    {
        Flip();
    }

    protected virtual void Attack()
    {
        if (attack_duration <= 0)
        {
            is_attack = false;
        }
        else
        {
            attack_duration = attack_duration - Time.deltaTime;
            anim.SetInteger("anim", 4); // attack animation
        }
    }

    protected virtual void Move()
    {
        Vector2 posA = transform.position;
        Vector2 posB = player_Script.transform.position;

        if (posA.x > posB.x)
            movement_speed = Mathf.Abs(movement_speed) * (-1f);
        else
            movement_speed = Mathf.Abs(movement_speed);
        transform.position = new Vector2(transform.position.x + movement_speed * Time.deltaTime, transform.position.y);
        anim.SetInteger("anim", 1);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(death_time_base);
        player_Script.TotalMonsters();
        Destroy(gameObject);
    }

    protected virtual void GetEffects() { return; }

    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(hurt_time_base);
        is_hurt = true;
        state = State.IDLE;
        GetEffects();
    }

    protected void Update()
    {
        if (player_Script.gameObject != null)
        {
            Vector2 posA = transform.position;
            Vector2 posB = player_Script.transform.position;
            float distance = Vector2.Distance(posA, posB);
            if (distance <= attack_distance && state != State.DEATH && state != State.HURT)
                state = State.ATTACK;
            //===============================//
            //            states
            //===============================//
            switch (state)
            {
                case State.IDLE:
                    anim.SetInteger("anim", 0);
                    break;
                case State.WALK:
                    if (is_move)
                        Move();
                    else
                        state = State.IDLE;
                    break;
                case State.DEATH:
                    anim.SetInteger("anim", 2);
                    StartCoroutine(Death());
                    break;
                case State.HURT:
                    if (is_hurt)
                    {
                        is_hurt = false;
                        anim.SetInteger("anim", 3);
                        StartCoroutine(Hurt());
                    }
                    break;
                case State.ATTACK:
                    if (is_attack)
                    {
                        Attack();
                    }
                    else
                    {
                        if (attack_cd <= 0)
                        {
                            is_attack = true;
                            attack_cd = attack_cooldown;
                            attack_duration = attack_duration_base;
                        }
                        else
                        {
                            attack_cd = attack_cd - Time.deltaTime;
                            anim.SetInteger("anim", 0);
                        }
                    }
                    break;
                default: break;
            }
        }
    }

    protected void FixedUpdate()
    {
        if (!PlayerController.allPause && state != State.DEATH && state != State.HURT)
        {
            Vector3 pos = transform.position + Vector3.right * agroDistance;
            Vector3 playerPos = transform.position;
            pos.y = pos.y - 1f;
            playerPos.y = playerPos.y - 1f;
            RaycastHit2D hit = Physics2D.Linecast(playerPos + Vector3.left * agroDistance, pos, 1 << LayerMask.NameToLayer("Player"));
            if (hit.collider != null)
                is_move = true;
            else
                is_move = false;
            state = State.WALK;
            Debug.DrawLine(playerPos + Vector3.left * agroDistance, pos, Color.blue);
        }
    }

    protected void Flip()
    {
        Vector2 position  = transform.position;
        Vector2 playerPos = player_Script.transform.position;
        if (position.x > playerPos.x)
        {
            if (!isLeftFlip)
                localScale.x = Mathf.Abs(localScale.x) * (-1f);
            else
                localScale.x = Mathf.Abs(localScale.x);
                movement_speed = Mathf.Abs(movement_speed) * (-1f);
        }
        else if (position.x < playerPos.x)
        {
            movement_speed = Mathf.Abs(movement_speed);
            if (!isLeftFlip)
                localScale.x = Mathf.Abs(localScale.x);
            else
                localScale.x = Mathf.Abs(localScale.x) * (-1f);
        }
        transform.localScale = localScale;
    }

    protected void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "attack":
                    attack_duration_base = clip.length;
                    break;
                case "hit":
                    hurt_time_base = clip.length;
                    break;
                case "death":
                    death_time_base = clip.length;
                    break;
                case "walk":
                    move_time_base = clip.length;
                    break;
            }
        }
    }
}
