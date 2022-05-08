using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    public  string sound;
    private AudioClip muz;
    private AudioSource audioSrc;
    public  static bool muzIsPlay = true;
    private bool stop = false;
    private static float duration = 0f;
    public void Start()
    {
            muzIsPlay = true;
            muz = Resources.Load("sound/Music/" + sound) as AudioClip;
            audioSrc = GetComponent<AudioSource>();
            float volume = PlayerPrefs.GetFloat("musicVoice");
            audioSrc.volume = volume;
        audioSrc.PlayOneShot(muz);
        audioSrc.time = 15;

    }

    void Update()
    {
        if (!audioSrc.isPlaying && audioSrc != null && muzIsPlay)
        {
            duration = 0f;
            audioSrc.PlayOneShot(muz);
        }
        else
        {
            duration = duration + Time.deltaTime;
        }
    }

}