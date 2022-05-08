using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : BossScript
{

    public GameObject hitSfx1;
    public GameObject hitSfx2;
    public GameObject hitSfx3;
    public GameObject hitSfx4;
    private enum Change { ATTACK1, ATTACK2,ATTACK3,ATTACK4 };
    private Change change;

    private int i = 0;

    private void Start()
    {
        RandomAttacks();
    }


    private void RandomAttacks()
    {
        i = i + 1;
        if (i > 4)
            i = 0;
        Debug.Log(i);

        switch (i)
        {
            case 1:
                change = Change.ATTACK1;
                attack_distance = 3f;
                break;
            case 2:
                change = Change.ATTACK2;
                attack_distance = 5.5f;
                break;
            case 3:
                change = Change.ATTACK3;
                attack_distance = 5.5f;
                break;
            case 4:
                change = Change.ATTACK4;
                attack_distance = 5.5f;
                break;
        }
    }
    protected override void GetEffects()
    {
        if (transform.position.x > player_Script.transform.position.x)
        {
            Instantiate(Resources.Load("bloodEffect/blueBlood") as GameObject, transform.position, Quaternion.Euler(0f, 90, 90f));
            Instantiate(Resources.Load("Effects/sfxSword") as GameObject, transform.position, Quaternion.Euler(0f, 90, 90f));
        }
        else
        {
            Instantiate(Resources.Load("bloodEffect/blueBlood") as GameObject, transform.position, Quaternion.Euler(-180, 90, 90f));
            Instantiate(Resources.Load("Effects/sfxSword") as GameObject, transform.position, Quaternion.Euler(-180, 90, 90f));
        }
        player_Script.Shake(0.20f, 0.20f);
    }

    private void OnDamage(Vector2 position, float damage, float radius)
    {
        Collider2D take = Physics2D.OverlapCircle(position, radius, 1 << LayerMask.NameToLayer("Player"));
        if (take != null)
            take.GetComponent<PlayerController>().TakeDamage(damage);
    }

    protected override void Attack()
    {
        if (attack_duration <= 0)
        {
            is_attack = false;
            switch (change)
            {
                case Change.ATTACK1:
                    player_Script.Shake(0.30f, 0.20f);
                    player_Script.rb.AddForce(new Vector2(localScale.x * 3f, 5f), ForceMode2D.Impulse);
                    Instantiate(hitSfx1, transform.GetChild(0).position, Quaternion.identity);
                    OnDamage(transform.GetChild(0).position,3f, 3f);
                    Sounds.PlaySound("S_X_chop");
                    break;

                case Change.ATTACK2:
                    Vector2 position = transform.GetChild(0).position;
                    OnDamage(position, 2f, 2f);
                    player_Script.Shake(0.45f, 0.22f);
                    player_Script.rb.AddForce(new Vector2(localScale.x * 3f, 5f), ForceMode2D.Impulse);
                    Instantiate(hitSfx2, position, Quaternion.identity);
                    OnDamage(transform.GetChild(0).position, 4f, 3.5f);
                    Sounds.PlaySound("S_fullsight");
                    break;
                case Change.ATTACK3:
                    OnDamage(transform.GetChild(0).position, 5f, 5f);
                    player_Script.Shake(0.45f, 0.22f);
                    player_Script.rb.AddForce(new Vector2(localScale.x * 3f, 5f), ForceMode2D.Impulse);
                    Instantiate(hitSfx3, transform.GetChild(0).position, Quaternion.identity);
                    OnDamage(transform.GetChild(0).position, 3f, 2.5f);
                    Sounds.PlaySound("S_Thunder_rage");
                    break;
                case Change.ATTACK4:
                    OnDamage(transform.GetChild(0).position, 6f, 5f);
                    player_Script.Shake(0.45f, 0.22f);
                    player_Script.rb.AddForce(new Vector2(localScale.x * 3f, 5f), ForceMode2D.Impulse);
                    Instantiate(hitSfx4, transform.GetChild(0).position, Quaternion.identity);
                    OnDamage(transform.GetChild(0).position, 3f, 2.5f);
                    Sounds.PlaySound("S_rubysun");
                    break;
            }
            RandomAttacks();
        }
        else
        {
            attack_duration = attack_duration - Time.deltaTime;
            anim.SetInteger("anim", 4); // attack animation
        }
    }
}
