using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StarScript : MonoBehaviour
{
    public float jump_impulse;
    private SpriteRenderer render;
    private Sprite[] Sprite = new Sprite[2];
    private PlayerController player_Script;
    private Transform getChild;
    private Rigidbody2D rigidBody;
    private static bool check;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        player_Script = FindObjectOfType<PlayerController>();

        rigidBody = player_Script.GetComponent<Rigidbody2D>();
        getChild = player_Script.transform.GetChild(0).GetComponent<Transform>();
        Sprite[0] = Resources.Load("Other/Yellow", typeof(Sprite)) as Sprite;
        Sprite[1] = Resources.Load("Other/Orange", typeof(Sprite)) as Sprite;
    }

    private void Update()
    {
        if (player_Script !=  null)
        {
            check = Physics2D.OverlapCircle(getChild.position, 0.25f, 1 << LayerMask.NameToLayer("jumpForce"));
            if (check)
            {
                if (render.sprite != Sprite[0])
                    render.sprite = Sprite[0];
                if (Input.GetKey(KeyCode.Space))
                {
                    rigidBody.velocity = new Vector2(0, 0);
                    player_Script.JumpR(jump_impulse);
                    Sounds.PlaySound("JumpN");
                }

            }
            else if (render.sprite != Sprite[1])
                render.sprite = Sprite[1];
        }
    }
}