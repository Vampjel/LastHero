using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bringerOfDeath : BossScript
{

    public GameObject hitSfx1;
    public GameObject hitSfx2;

    private enum Change { ATTACK1, ATTACK2 };
    private Change change;

    private void Start()
    {
        RandomAttacks();
    }

    private void RandomAttacks()
    {
        int i = Random.RandomRange(1, 3);
        switch (i)
        {
            case 1:
                change = Change.ATTACK1;
                attack_distance = 2f;
                break;
            case 2:
                change = Change.ATTACK2;
                attack_distance = 2.5f;
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
                    player_Script.rb.AddForce(new Vector2(localScale.x * 5f, 12f), ForceMode2D.Impulse);
                    Instantiate(hitSfx1, transform.GetChild(0).position, Quaternion.identity);
                    OnDamage(transform.GetChild(0).position, 6f, 3f);
                    Sounds.PlaySound("S_desperate");

                   break;

                case Change.ATTACK2:
                    Sounds.PlaySound("S_Cold_bark");

                    OnDamage(transform.GetChild(0).position, 2f, 2f);
                    player_Script.Shake(0.45f, 0.22f);
                    player_Script.rb.AddForce(new Vector2(localScale.x * 5f, 12f), ForceMode2D.Impulse);
                    Instantiate(hitSfx2, transform.GetChild(0).position, Quaternion.identity);
                    OnDamage(transform.GetChild(0).position, 4f, 2.5f);
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
