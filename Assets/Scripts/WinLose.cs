using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinLose : MonoBehaviour
{
    public Canvas lose, win;
    private bool activate = false;
    private float xx, yy;
    private float size = 3f;
    private bool  check = true;
    private int current_level;
    public static bool showWinStage;


    private void Start()
    {
        showWinStage = true;
    }

    private void Update()
    {
        float x, y, angle;

        if (activate && check)
        {
            if (size <= 0f)
            {
                check = false;
                win.enabled = true;
                music.muzIsPlay = false;
                int current_level = PlayerPrefs.GetInt("LevelComplite");
                int build_index = SceneManager.GetActiveScene().buildIndex + 1;

                if (current_level < build_index)
                    LevelScript.nextSaveLevel(build_index);

                PlayerController.allPause = true;
            }
            else
            {
                showWinStage = false;
                size = size - 1.5f * Time.deltaTime;
                x = transform.position.x;
                y = transform.position.y;
                angle = Mathf.Rad2Deg * Mathf.Atan2(yy - y, xx - x);
                x = x + 1.5f * Time.deltaTime * Mathf.Cos(angle * Mathf.Deg2Rad);
                y = y + 1.5f * Time.deltaTime * Mathf.Sin(angle * Mathf.Deg2Rad);
                transform.Rotate(new Vector3(0, 0, -1.5f));
                transform.position = new Vector2(x, y);
                transform.localScale = new Vector3(size, size, size);
            }

        }
    }

    public void Lose()
    {
        Sounds.PlaySound("lose");
        lose.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Win")
        {
            music.muzIsPlay = false;
            Sounds.PlaySound("win");
            activate = true;
            xx = collision.transform.position.x;
            yy = collision.transform.position.y;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().isKinematic = true;
            PlayerController.allPause = true;
        }
    }
}
