using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSoundVolume : MonoBehaviour
{
    private AudioSource audioSrc;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        float volume = PlayerPrefs.GetFloat("musicVoice");
        audioSrc.volume = volume;
    }
}
