using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilaScript : MonoBehaviour
{
    public  float Speed;
    public  float Rotation_Speed;
    public float min_x, max_x;

    private PlayerController player_Script;

    private float effect_interval = 0f;
    private void Start()
    {
        player_Script = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float distance = x - player_Script.transform.position.x;

            if (effect_interval <= 0f)
            {
                effect_interval = 0.30f;
                Instantiate(Resources.Load("Effects/brush"), new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
            }
            else
            {
                effect_interval = effect_interval - Time.fixedDeltaTime;
            }

            if (min_x > x)
                Speed = Mathf.Abs(Speed);
            else if (max_x < x)
                Speed = Mathf.Abs(Speed) * (-1f);
            transform.position = new Vector3(x + Speed * Time.deltaTime, y, 5);

            if (Speed > 0)
                transform.Rotate(new Vector3(0, 0, Rotation_Speed));
            else if (Speed < 0)
                transform.Rotate(new Vector3(0, 0, -Rotation_Speed));
        }
    }

