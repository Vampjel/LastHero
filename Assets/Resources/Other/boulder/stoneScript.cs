using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneScript : MonoBehaviour
{

    public float speed;
    public float min_range;
    public float max_range;
    public float collider_size;

    public float x_left, x_right;

    private Rigidbody2D rb;
    private bool groundCheck;
    private bool radiusCheck;
    private PlayerController player_Script;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player_Script = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        groundCheck = Physics2D.OverlapCircle(transform.position, collider_size, 1 << LayerMask.NameToLayer("ground"));
        if (groundCheck && player_Script != null)
        {
            rb.velocity = new Vector2(0, 0);

            if (transform.position.x < x_left)
                speed = Mathf.Abs(speed);
            else if (transform.position.x > x_right)
                speed = Mathf.Abs(speed) * (-1);

            rb.AddForce(new Vector2(0, Random.RandomRange(min_range, max_range)), ForceMode2D.Impulse);
            if (Vector2.Distance(transform.position, player_Script.transform.position) <= 26f) ;
            Instantiate(Resources.Load("Effects/dustStone") as GameObject, transform.position, Quaternion.identity);
        }
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.isDeath = true;
            player_Script.onSetDeathTime();
        }

    }
}
