using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pong : MonoBehaviour
{
    [SerializeField] public float Speed;
    [SerializeField] public bool  x_pos;

    private PlayerController player_Script;

    private void Start()
    {
        player_Script = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(x_pos)
            transform.position = new Vector2(transform.position.x + Speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            if (Speed > 0)
                Speed = Mathf.Abs(Speed) * (-1f);
            else if(Speed < 0)
                Speed = Mathf.Abs(Speed);
        }
        else if(collision.gameObject.tag == "Player")
        {
            player_Script.TakeDamage(1000f);
        }

    }

}
