using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellPlatform : MonoBehaviour
{
    public float Impulse;
    private Rigidbody2D rb;
    private Vector2 box_collider_size;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box_collider_size = GetComponent<BoxCollider2D>().size;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, Impulse), ForceMode2D.Impulse);
        }
    }
}
