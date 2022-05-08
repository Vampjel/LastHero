using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float angle_speed;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, angle_speed));
    }
}
