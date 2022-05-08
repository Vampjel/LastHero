using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grib : MonoBehaviour
{
    public float Jump_Impulse;
    private PlayerController player_Script;
    private CapsuleCollider2D capsule_size;
    void Start()
    {
        player_Script = FindObjectOfType<PlayerController>();
        capsule_size = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player_Script.JumpR(Jump_Impulse);
            Instantiate(Resources.Load("Effects/S_Green_invade"), player_Script.transform.GetChild(0).position, Quaternion.identity);
        }
    }
}
