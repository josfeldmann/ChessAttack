using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public AudioClip defaultClip;
    private static MusicPlayer instance = null;
    public AudioSource bgMusic;

    private void Awake() {
        if (instance == null) {
            instance = this;
            PlayStartingSong();
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }



    public void PlayStartingSong() {
        bgMusic.clip = defaultClip;
        bgMusic.Play();
    }


    


}
