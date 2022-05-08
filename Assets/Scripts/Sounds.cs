using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static AudioClip jumpSound, JumpSoundN, hit, slash1, slash2, slash3, slash4, coin, box, run, S_land_snaze,
    win, lose;


    public static AudioClip S_Blood_splash, S_Combo_knock, S_chilly_kick, S_Blue_Hole, S_Cold_bark, S_desperate, S_X_chop, S_fullsight, S_Thunder_rage, S_rubysun;

    static AudioSource audioSrc;

    static float soundVolume;
    void Awake()
    {
        jumpSound  = Resources.Load("sound/effects/jump")  as AudioClip;
        JumpSoundN = Resources.Load("sound/effects/JumpN") as AudioClip;

        slash1 = Resources.Load("sound/effects/slash1") as AudioClip;
        slash2 = Resources.Load("sound/effects/slash2") as AudioClip;
        slash3 = Resources.Load("sound/effects/slash3") as AudioClip;
        slash4 = Resources.Load("sound/effects/slash4") as AudioClip;

        run = Resources.Load("sound/effects/run") as AudioClip;
        coin = Resources.Load("sound/effects/coinSound") as AudioClip;
        box  = Resources.Load("sound/effects/boxSound") as AudioClip;
        win  = Resources.Load("sound/effects/win") as AudioClip;
        lose = Resources.Load("sound/effects/lose") as AudioClip;

        S_land_snaze = Resources.Load("sound/effects/S_land_snaze") as AudioClip;

        // boss sounds

        S_Blood_splash = Resources.Load("BossEffects/Sound/S_Blood_splash") as AudioClip;
        S_Combo_knock = Resources.Load("BossEffects/Sound/S_Combo_knock") as AudioClip;

        S_chilly_kick  = Resources.Load("BossEffects/Sound/S_Combo_knock") as AudioClip;
        S_Blue_Hole  = Resources.Load("BossEffects/Sound/S_Blue_Hole") as AudioClip; ;

        S_Cold_bark = Resources.Load("BossEffects/Sound/S_Cold_bark") as AudioClip;
        S_desperate = Resources.Load("BossEffects/Sound/S_desperate") as AudioClip; ;



        S_X_chop = Resources.Load("BossEffects/Sound/S_X_chop") as AudioClip; ;
        S_fullsight = Resources.Load("BossEffects/Sound/S_fullsight") as AudioClip; ;
        S_Thunder_rage = Resources.Load("BossEffects/Sound/S_Thunder_rage") as AudioClip; ;
        S_rubysun = Resources.Load("BossEffects/Sound/S_rubysun") as AudioClip; ;
        audioSrc = GetComponent<AudioSource>();

        float volume = PlayerPrefs.GetFloat("effectVoice");
        soundVolume = 0.75f;
        audioSrc.volume = volume;
    }
    public static void PlaySound(string clip)
    {

        switch (clip)
        {
            case "S_X_chop":
                audioSrc.PlayOneShot(S_X_chop); break;
            case "S_fullsight":
                audioSrc.PlayOneShot(S_fullsight); break;
            case "S_Cold_bark":
                audioSrc.PlayOneShot(S_Cold_bark); break;
            case "S_Thunder_rage":
                audioSrc.PlayOneShot(S_Thunder_rage); break;
            case "S_rubysun":
                audioSrc.PlayOneShot(S_rubysun); break;
            case "S_desperate":
                audioSrc.PlayOneShot(S_desperate); break;
            case "S_land_snaze":
                audioSrc.PlayOneShot(S_land_snaze); break;
            case "S_chilly_kick":
                audioSrc.PlayOneShot(S_chilly_kick); break;
            case "S_Blue_Hole":
                audioSrc.PlayOneShot(S_Blue_Hole); break;
            case "S_Blood_splash":
                audioSrc.PlayOneShot(S_Blood_splash); break;
            case "S_Combo_knock":
                audioSrc.PlayOneShot(S_Combo_knock); break;
            case "slash1":
                audioSrc.PlayOneShot(slash1); break;
            case "slash2":
               audioSrc.PlayOneShot(slash2); break;
            case "slash3":
                 audioSrc.PlayOneShot(slash3); break;
            case "slash4":
                 audioSrc.PlayOneShot(slash4); break;
            case "jump": 
                audioSrc.PlayOneShot(jumpSound) ; break;
            case "JumpN": 
                audioSrc.PlayOneShot(JumpSoundN); break;
            case "coin":
                audioSrc.PlayOneShot(coin); break;
            case "box":
                audioSrc.PlayOneShot(box); break;
            case "win":
                audioSrc.PlayOneShot(win); break;
            case "lose":
                audioSrc.PlayOneShot(lose); break;
            case "run":
                if(!audioSrc.isPlaying ) audioSrc.PlayOneShot(run); break;

        }
    }


}
