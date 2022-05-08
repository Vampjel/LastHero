using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformROck : MonoBehaviour
{
    public float platform_waint;
    public float gravity;
    public LayerMask layer;
    private Rigidbody2D rigidBody;
    private bool is_active = true;
    private PlayerController player_Script;
    private Transform player;
    private float tick = 0f;
    private float interval = 0f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        rigidBody.gravityScale = Mathf.Abs(gravity);
    }

    void LandSnaze()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float distance = transform.position.x - player.transform.position.x;

        if (tick <= 0)
        {
            is_active = true;
        }
        else
        {
            tick = tick - Time.deltaTime;
        }

        if (is_active && distance <= 10f && distance > -10f)
        {
            is_active = false;
            tick = platform_waint;
            bool check = Physics2D.OverlapBox(transform.position, new Vector2(10,10), 0, 1 << LayerMask.NameToLayer("ground"));

            if (check)
            {
                if (rigidBody.gravityScale > 0f)
                {
                    rigidBody.gravityScale = Mathf.Abs(gravity) * -1f;
                }
                else
                {
                    rigidBody.gravityScale = Mathf.Abs(gravity);

                }
                rigidBody.velocity = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if(interval <= 0f)
        {
            interval = 0.1f;
            LandSnaze();
        }
        else
        {
            interval = interval - Time.fixedDeltaTime;
        }
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject b = null;
        if (collision.gameObject.tag == "player")
        {
            FindObjectOfType<PlayerController>().TakeDamage(1000);
        }
        else if (rigidBody.gravityScale > 0)
        {
            Sounds.PlaySound("S_land_snaze");
            b = Instantiate(Resources.Load("Effects/S_land_snaze") as GameObject, new Vector3(transform.position.x, transform.position.y - 1.2f, 10f), Quaternion.identity);
        }
    }
}
