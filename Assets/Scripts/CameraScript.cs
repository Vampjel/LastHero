using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform who;
    public float x_left, x_right;
    public float y_down, y_up;
    private Vector3 position;
    public static bool isEnable = true;
    void Update()
    {
        if (isEnable)
        {
            if (who != null)
            {
                position = who.position;
                if (position.x < x_left)
                    position.x = x_left;
                else if (position.x > x_right)
                    position.x = x_right;

                if (position.y < y_down)
                    position.y = y_down;
                else if (position.y > y_up)
                    position.y = y_up;
                position.z = -10f;
                transform.position = Vector3.Lerp(transform.position, position, 3f * Time.deltaTime);
            }
        }
    }
}
