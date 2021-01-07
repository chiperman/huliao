using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musiccontrol : MonoBehaviour {

    public AudioSource music;

    public void PlayMusic()
    {
        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }
}