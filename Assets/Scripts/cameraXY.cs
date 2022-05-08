using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraXY : MonoBehaviour
{
    public float x_left, y_down;
    public float x_right, y_up;
    CameraScript cameraScript;
    private void Start()
    {
        cameraScript = FindObjectOfType<CameraScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cameraScript.x_left  = this.x_left;
            cameraScript.x_right = this.x_right;

            cameraScript.y_up    = this.y_up;
            cameraScript.y_down  = this.y_down;
        }
    }
}
