using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private AudioSource music;
    
    public AudioClip newMusic;
    
    // Start is called before the first frame update
    void Start() {
        music = GetComponent<AudioSource>();
    }

    public void SwitchTracks() {
        float currentTime = 0;
        float start = music.volume;

        while (currentTime < 50)
        {
            currentTime += Time.deltaTime;
            music.volume = Mathf.Lerp(start, 0, currentTime / 50);
        }

        music.clip = newMusic;
        music.Play();
        currentTime = 0;
        while (currentTime < 50)
        {
            currentTime += Time.deltaTime;
            music.volume = Mathf.Lerp(0, start, currentTime / 50);
        }
    }
}
