using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update 
    public Sound[] sounds;
    private static AudioManager audioManagerInstance;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (audioManagerInstance == null)
        {
            audioManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.loop;

            if (s.source.playOnAwake)
                s.source.Play();
        }

    }

    public Sound GetSource(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.clip, s.volume);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
