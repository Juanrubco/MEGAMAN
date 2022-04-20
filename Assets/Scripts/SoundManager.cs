using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip  bullet, death;
    static AudioSource audioSrc;
    private static string LastSound;
    // Start is called before the first frame update
    void Start()
    {
        bullet = Resources.Load<AudioClip>("bullet");
        death = Resources.Load<AudioClip>("death");
        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        LastSound = clip;
        switch (clip)
        {
            case "PlayerBullet":
                audioSrc.PlayOneShot(bullet);
                break;
            case "PlayerDeath":
                audioSrc.PlayOneShot(death);
                break;
        }


    }

    public static void StopSound(string clip)
    {
        if (LastSound == clip)
            audioSrc.Stop();
    }
}
