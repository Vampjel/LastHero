using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missible : MonoBehaviour
{
    public float speed, distance, damage;
    private PlayerController player_Script;
    private Vector3 localScale;
    private void Start()
    {
        localScale = transform.localScale;
        player_Script = FindObjectOfType<PlayerController>();
        if (transform.position.x < player_Script.transform.position.x)
            localScale.x = Mathf.Abs(localScale.x) * (-1f);
        transform.localScale = localScale;
    }
    public void setVariables(float speed, float distance, float damage)
    {
        this.speed = speed;
        this.distance = distance;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        if (distance <= 0f)
        {
            Destroy(gameObject, GetComponent<ParticleSystem>().duration);
        }
        else
        {
            distance = distance - Time.deltaTime;
            bool groundCheck = Physics2D.OverlapCircle(transform.position, 0.4f, 1 << LayerMask.NameToLayer("ground"));
            bool damage = Physics2D.OverlapCircle(transform.position, 0.4f, 1 << LayerMask.NameToLayer("Player"));
            if (groundCheck)
                distance = 0f;
            if (damage)
            {
                player_Script.TakeDamage(this.damage);
                player_Script.Shake(0.10f, 0.20f);
                distance = 0f;
            }
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
    }

}
