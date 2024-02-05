using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
        _audio = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip _clip)
    {
        _audio.PlayOneShot(_clip);
    }
}
