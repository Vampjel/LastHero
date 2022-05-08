using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainOff : MonoBehaviour
{
    [SerializeField] public float Delay;
    private SpringJoint2D springJoint;

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(Delay);
        springJoint.enabled = false;
        Destroy(gameObject,3.5f);
    }

    void Start()
    {
        springJoint = GetComponent<SpringJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(delay());
        }
    }

}
